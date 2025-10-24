namespace Identity.Persistence.Options;

public class JwtTokenOptions
{
    public string? Issuer { get; init; }
    public string? Audience { get; init; }
    public string? Key { get; init; }
    public int ExpireInMinute { get; init; }
}