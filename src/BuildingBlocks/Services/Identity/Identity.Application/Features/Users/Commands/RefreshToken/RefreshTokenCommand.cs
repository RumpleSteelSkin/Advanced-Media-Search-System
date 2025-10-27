using Core.Application.Base.BaseResult;
using MediatR;

namespace Identity.Application.Features.Users.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<BaseResult<RefreshTokenCommandResponseDto>>;