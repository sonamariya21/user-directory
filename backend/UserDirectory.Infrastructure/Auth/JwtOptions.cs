namespace UserDirectory.Infrastructure.Auth;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    public string? Authority { get; set; }
    public string Issuer { get; set; } = "https://localhost:7189";
    public string Audience { get; set; } = "user-directory-api";
    public string SigningKey { get; set; } = string.Empty;
    public int ExpirationMinutes { get; set; } = 60;
    public bool UseOidc => !string.IsNullOrWhiteSpace(Authority);
}
