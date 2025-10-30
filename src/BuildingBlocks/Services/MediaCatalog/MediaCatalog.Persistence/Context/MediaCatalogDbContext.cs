using System.Reflection;
using MediaCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaCatalog.Persistence.Context;

public class MediaCatalogDbContext(DbContextOptions<MediaCatalogDbContext> options) : DbContext(options)
{
    DbSet<MediaFileObject> MediaFileObjects { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}