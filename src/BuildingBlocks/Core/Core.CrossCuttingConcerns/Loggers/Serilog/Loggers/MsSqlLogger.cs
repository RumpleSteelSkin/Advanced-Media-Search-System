using Core.Configuration.Settings;
using Core.CrossCuttingConcerns.Loggers.Serilog.Base;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Loggers.Serilog.Loggers;

public class MsSqlLogger : LoggerService
{
    public MsSqlLogger(IConfiguration configuration)
    {
        var logConfig = configuration.GetSection(nameof(SerilogLogConfigurations)).Get<SerilogLogConfigurations>() ??
                        throw new InvalidOperationException("SerilogLogConfigurations not found");

        Logger = new LoggerConfiguration().WriteTo.MSSqlServer(logConfig.ConnectionString,
                new MSSqlServerSinkOptions
                    { TableName = logConfig.TableName, AutoCreateSqlTable = logConfig.AutoCreateSqlTable })
            .CreateLogger();
    }
}