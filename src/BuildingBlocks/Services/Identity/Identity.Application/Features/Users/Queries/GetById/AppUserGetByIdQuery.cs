using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Users.Queries.GetById;

public class AppUserGetByIdQuery:IRequest<BaseResult<AppUserGetByIdQueryResponseDto>>
{
    public Guid Id { get; set; }
}

public class AppUserGetByIdQueryHandler(UserManager<AppUser> userManager):IRequestHandler<AppUserGetByIdQuery,BaseResult<AppUserGetByIdQueryResponseDto>>
{
    public Task<BaseResult<AppUserGetByIdQueryResponseDto>> Handle(AppUserGetByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class AppUserGetByIdQueryResponseDto
{
    
}