using AutoMapper;
using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Users.Queries.GetAll;

public class AppUserGetAllQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    : IRequestHandler<AppUserGetAllQuery, BaseResult<IEnumerable<AppUserGetAllQueryResponseDto>>>
{
    public async Task<BaseResult<IEnumerable<AppUserGetAllQueryResponseDto>>> Handle(AppUserGetAllQuery request,
        CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
        var response = mapper.Map<IEnumerable<AppUserGetAllQueryResponseDto>>(users);
        return BaseResult<IEnumerable<AppUserGetAllQueryResponseDto>>.Success(response);
    }
}