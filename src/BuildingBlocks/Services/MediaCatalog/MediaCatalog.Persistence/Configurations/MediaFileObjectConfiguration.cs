using MediaCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaCatalog.Persistence.Configurations;

public class MediaFileObjectConfiguration : IEntityTypeConfiguration<MediaFileObject>
{
    public void Configure(EntityTypeBuilder<MediaFileObject> builder)
    {
        builder.ToTable("MediaFileObjects");
    }
}