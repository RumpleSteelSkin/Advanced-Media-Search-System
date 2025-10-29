using FileService.Domain.Interfaces;
using MediatR;

namespace FileService.Application.Features.Queries.GetFileObjectDetail;

public class GetFileObjectDetailByObjectNameQueryHandler(IFileStorageService fileStorageService)
    : IRequestHandler<GetFileObjectDetailByObjectNameQuery, string>
{
    public async Task<string> Handle(GetFileObjectDetailByObjectNameQuery request, CancellationToken cancellationToken)
    {
        return await fileStorageService.GetObjectDetailAsync(request.ObjectName, cancellationToken);
    }
}