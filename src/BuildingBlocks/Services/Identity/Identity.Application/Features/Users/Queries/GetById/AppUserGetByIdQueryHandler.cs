using AutoMapper;
using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Users.Queries.GetById;

public class AppUserGetByIdQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    : IRequestHandler<AppUserGetByIdQuery, BaseResult<AppUserGetByIdQueryResponseDto>>
{
    public async Task<BaseResult<AppUserGetByIdQueryResponseDto>> Handle(AppUserGetByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null) return BaseResult<AppUserGetByIdQueryResponseDto>.NotFound("User not found");
        var response = mapper.Map<AppUserGetByIdQueryResponseDto>(user);
        return BaseResult<AppUserGetByIdQueryResponseDto>.Success(response);
    }
}