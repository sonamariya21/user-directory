namespace UserDirectory.Application.Abstractions;

public interface ITokenService
{
    string CreateAccessToken(string username, IEnumerable<string>? roles = null);
}
