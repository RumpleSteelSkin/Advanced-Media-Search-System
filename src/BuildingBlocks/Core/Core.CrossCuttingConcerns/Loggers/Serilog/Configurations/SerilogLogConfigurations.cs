namespace Core.CrossCuttingConcerns.Loggers.Serilog.Configurations;

public class SerilogLogConfigurations
{
    public string? ConnectionString { get; init; }
    public string? TableName { get; init; }
    public bool AutoCreateSqlTable { get; init; }
}