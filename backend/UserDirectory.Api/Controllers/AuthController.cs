using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserDirectory.Application.Abstractions;
using UserDirectory.Application.DTOs;
using UserDirectory.Infrastructure.Auth;

namespace UserDirectory.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ITokenService tokenService, IOptions<JwtOptions> jwtOptions) : ControllerBase
{
    /// <summary>
    /// Demo credentials: admin / Admin@123
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<TokenResponseDto> Login([FromBody] LoginRequestDto request)
    {
        if (jwtOptions.Value.UseOidc)
        {
            return BadRequest(new
            {
                message = "Local login is disabled. Obtain a Bearer token from the provider."
            });
        }

        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Username and password are required." });
        }

        // Demo-only auth. Replace with real user store / IdP in production.
        if (!string.Equals(request.Username, "admin", StringComparison.OrdinalIgnoreCase)
            || request.Password != "Admin@123")
        {
            return Unauthorized(new { message = "Invalid username or password." });
        }

        var accessToken = tokenService.CreateAccessToken(request.Username, ["Admin"]);
        var expiresIn = jwtOptions.Value.ExpirationMinutes * 60;

        return Ok(new TokenResponseDto
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            ExpiresInSeconds = expiresIn
        });
    }
}
