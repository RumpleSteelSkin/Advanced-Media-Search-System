using Identity.Domain.Entities;
using Identity.Persistence.Context;
using Identity.Persistence.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Identity.Persistence.Extensions;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Entity Framework Services

        services.AddDbContext<IdentityContext>((serviceProvider, options) =>
        {
            var dbSettings = serviceProvider.GetRequiredService<IOptions<IdentityDbConnection>>().Value;
            if (string.IsNullOrWhiteSpace(dbSettings.ConnectionString))
                throw new InvalidOperationException($"{nameof(IdentityContext)} connection string is not set!");
            options.UseSqlServer(dbSettings.ConnectionString);
        });

        services.AddScoped<DbContext, IdentityContext>(sp =>
            sp.GetRequiredService<IdentityContext>()
        );

        services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        #endregion
    }
}