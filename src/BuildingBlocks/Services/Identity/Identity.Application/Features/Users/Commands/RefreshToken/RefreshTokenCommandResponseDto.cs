namespace Identity.Application.Features.Users.Commands.RefreshToken;

public class RefreshTokenCommandResponseDto
{
    public string? Token { get; set; }
    public DateTime Expire { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpire { get; set; }
}