using AutoMapper;
using Core.Application.Base.BaseResult;
using MediaCatalog.Application.Services.Repositories;
using MediaCatalog.Domain.Entities;
using MediatR;

namespace MediaCatalog.Application.Features.MediaFileObjects.Commands.Create;

public class CreateMediaFileObjectCommandHandler(IMediaFileObjectRepository repository, IMapper mapper)
    : IRequestHandler<CreateMediaFileObjectCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(CreateMediaFileObjectCommand request,
        CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<MediaFileObject>(request);
        var response = await repository.AddAsync(mapped, cancellationToken);
        return response ? BaseResult<object>.Success(true) : BaseResult<object>.Fail("Media file objects not added");
    }
}