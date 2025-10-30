using Core.Application.Base.BaseResult;
using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace MediaCatalog.Application.Features.MediaFileObjects.Commands.Delete;

public record DeleteMediaFileObjectCommand(Guid Id) : IRequest<BaseResult<object>>, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}