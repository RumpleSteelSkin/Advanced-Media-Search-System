using Core.Application.Base.BaseResult;
using MediatR;

namespace Identity.Application.Features.Users.Queries.Login;

public record AppUserLoginQuery(string UserNameOrEmail, string Password)
    : IRequest<BaseResult<AppUserLoginQueryResponseDto>>;