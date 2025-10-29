using AutoMapper;
using Core.Application.Base.BaseResult;
using Identity.Application.Contracts;
using Identity.Application.Features.Shared.DTOs;
using Identity.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Users.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IMapper mapper,
    IJwtService jwtService,
    IdentityContext context)
    : IRequestHandler<RefreshTokenCommand, BaseResult<RefreshTokenCommandResponseDto>>
{
    public async Task<BaseResult<RefreshTokenCommandResponseDto>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var token = await context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == request.RefreshToken, cancellationToken);

        if (token == null)
            return BaseResult<RefreshTokenCommandResponseDto>.Fail("Refresh token not found");

        if (token.IsUsed || token.IsRevoked)
            return BaseResult<RefreshTokenCommandResponseDto>.Fail("Token already used or revoked");

        if (token.Expires < DateTime.UtcNow)
            return BaseResult<RefreshTokenCommandResponseDto>.Fail("Token expired");

        if (token.User == null)
            return BaseResult<RefreshTokenCommandResponseDto>.Fail("User not found");

        token.IsUsed = true;
        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);

        var userDto = new UserDto
        {
            UserName = token.User.UserName,
            Email = token.User.Email,
            Id = token.User.Id.ToString()
        };

        var newTokens = await jwtService.GenerateTokenAsync(userDto);
        if (newTokens is null)
            return BaseResult<RefreshTokenCommandResponseDto>.Fail("Token generation failed");

        return BaseResult<RefreshTokenCommandResponseDto>.Success(
            mapper.Map<RefreshTokenCommandResponseDto>(newTokens)!);
    }
}