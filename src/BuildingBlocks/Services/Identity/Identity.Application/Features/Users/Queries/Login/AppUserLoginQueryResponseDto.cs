namespace Identity.Application.Features.Users.Queries.Login;

public class AppUserLoginQueryResponseDto
{
    public string? Token { get; set; }
    public DateTime Expire { get; set; }
}