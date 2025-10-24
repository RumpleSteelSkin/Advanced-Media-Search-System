using Core.CrossCuttingConcerns.Loggers.Serilog.Base;
using Core.CrossCuttingConcerns.Loggers.Serilog.Configurations;
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

        Logger = new LoggerConfiguration().WriteTo.MSSqlServer(connectionString: logConfig.ConnectionString,
                sinkOptions: new MSSqlServerSinkOptions
                    { TableName = logConfig.TableName, AutoCreateSqlTable = logConfig.AutoCreateSqlTable })
            .CreateLogger();
    }
}