using AutoMapper;
using MediaCatalog.Application.Features.MediaFileObjects.Commands.Create;
using MediaCatalog.Domain.Entities;

namespace MediaCatalog.Application.Features.MediaFileObjects.Mappers;

public class MediaFileObjectMapper : Profile
{
    public MediaFileObjectMapper()
    {
        CreateMap<CreateMediaFileObjectCommand, MediaFileObject>();
    }
}