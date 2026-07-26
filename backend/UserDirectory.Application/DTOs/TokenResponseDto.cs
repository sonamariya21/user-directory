namespace UserDirectory.Application.DTOs;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = "Bearer";
    public int ExpiresInSeconds { get; set; }
}
