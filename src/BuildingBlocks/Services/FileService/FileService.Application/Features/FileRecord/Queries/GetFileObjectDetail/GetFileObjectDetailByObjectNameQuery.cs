using MediatR;

namespace FileService.Application.Features.FileRecord.Queries.GetFileObjectDetail;

public record GetFileObjectDetailByObjectNameQuery(string ObjectName) : IRequest<string>;