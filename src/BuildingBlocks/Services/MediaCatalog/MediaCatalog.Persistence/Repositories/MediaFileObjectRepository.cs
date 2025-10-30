using Core.Persistence.Repositories;
using MediaCatalog.Application.Services.Repositories;
using MediaCatalog.Domain.Entities;
using MediaCatalog.Persistence.Context;

namespace MediaCatalog.Persistence.Repositories;

public class MediaFileObjectRepository(MediaCatalogDbContext context)
    : EntityFrameworkRepositoryBase<MediaFileObject, Guid, MediaCatalogDbContext>(context), IMediaFileObjectRepository;