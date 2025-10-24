using Serilog;

namespace Core.CrossCuttingConcerns.Loggers.Serilog.Base;

public class LoggerService(ILogger logger)
{
    protected LoggerService() : this(null!) { }
    protected ILogger Logger { get; init; } = logger;

    public void Verbose(string message) => Logger.Verbose(message);
    public void Info(string message) => Logger.Information(message);
    public void Error(string message) => Logger.Error(message);
    public void Debug(string message) => Logger.Debug(message);
    public void Warning(string message) => Logger.Warning(message);
}