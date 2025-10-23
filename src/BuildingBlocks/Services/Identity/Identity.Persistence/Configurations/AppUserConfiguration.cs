using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(z => z.FirstName).HasMaxLength(50);
        builder.Property(z => z.LastName).HasMaxLength(50);
        builder.Property(z => z.Created).IsRequired();
    }
}