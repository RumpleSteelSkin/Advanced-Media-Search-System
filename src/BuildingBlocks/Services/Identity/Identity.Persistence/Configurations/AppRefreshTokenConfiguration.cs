using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.Configurations;

public class AppRefreshTokenConfiguration : IEntityTypeConfiguration<AppRefreshToken>
{
    public void Configure(EntityTypeBuilder<AppRefreshToken> builder)
    {
        builder
            .HasKey(r => r.Id);
        
        builder
            .HasOne(r => r.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r => r.Token).IsRequired();
        builder.Property(r => r.Expires).IsRequired();
        builder.Property(r => r.IsUsed).HasDefaultValue(false);
        builder.Property(r => r.IsRevoked).HasDefaultValue(false);
    }
}