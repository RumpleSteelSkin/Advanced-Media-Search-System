namespace Core.CrossCuttingConcerns.Loggers.Serilog.Models;

public class LogParameter
{
    public object? Value { get; set; }

    public string? Type { get; set; }
}