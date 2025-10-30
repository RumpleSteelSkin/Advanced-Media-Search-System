using Core.Persistence.Repositories;
using MediaCatalog.Domain.Entities;

namespace MediaCatalog.Application.Services.Repositories;

public interface IMediaFileObjectRepository : IAsyncRepository<MediaFileObject, Guid>;