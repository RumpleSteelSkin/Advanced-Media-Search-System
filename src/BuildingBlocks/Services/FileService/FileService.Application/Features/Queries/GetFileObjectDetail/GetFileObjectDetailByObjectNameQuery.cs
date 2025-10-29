using MediatR;

namespace FileService.Application.Features.Queries.GetFileObjectDetail;

public record GetFileObjectDetailByObjectNameQuery(string ObjectName) : IRequest<string>;