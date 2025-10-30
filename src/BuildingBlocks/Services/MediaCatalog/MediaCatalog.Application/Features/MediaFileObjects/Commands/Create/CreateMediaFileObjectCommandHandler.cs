using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediaCatalog.Application.Services.Repositories;
using MediaCatalog.Domain.Entities;
using MediatR;

namespace MediaCatalog.Application.Features.MediaFileObjects.Commands.Create;

public class CreateMediaFileObjectCommandHandler(IMediaFileObjectRepository repository, IMapper mapper)
    : IRequestHandler<CreateMediaFileObjectCommand, string>
{
    public async Task<string> Handle(CreateMediaFileObjectCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<MediaFileObject>(request);
        var response = await repository.AddAsync(mapped, cancellationToken);
        return response
            ? "File added successfully"
            : throw new BusinessException("Media file object was not added successfully");
    }
}