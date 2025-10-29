namespace Core.Configuration.Settings;

public class RabbitMqSettings
{
    public string? HostName { get; set; }
    public string? VirtualHost { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public ushort Port { get; set; }

    public int RetryCount { get; set; }
}