using Identity.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Presentation.Services;

public class RefreshTokenCleanupService(IServiceScopeFactory scopeFactory, ILogger<RefreshTokenCleanupService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("RefreshTokenCleanupService started.");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();

                var now = DateTime.UtcNow;
                var expiredTokens = await context.RefreshTokens
                    .Where(r => r.Expires < now || r.IsUsed || r.IsRevoked)
                    .ToListAsync(stoppingToken);

                if (expiredTokens.Count > 0)
                {
                    context.RefreshTokens.RemoveRange(expiredTokens);
                    await context.SaveChangesAsync(stoppingToken);
                    logger.LogInformation("Deleted {Count} expired or invalid refresh tokens.", expiredTokens.Count);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while cleaning up refresh tokens.");
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }

        logger.LogInformation("RefreshTokenCleanupService stopped.");
    }
}