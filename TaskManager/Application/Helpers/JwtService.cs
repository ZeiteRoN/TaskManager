using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Models;

namespace TaskManager.Application.Helpers;

public class JwtService : IJwtService
{
    public readonly string _secret;
    public readonly string _issuer;
    public readonly string _audience;
    public readonly int _expirationMinutes;

    public JwtService(IConfiguration configuration)
    {
        // Перевірка наявності кожного з параметрів у конфігурації
        _secret = configuration["JwtSettings:Secret"];
        _issuer = configuration["JwtSettings:Issuer"];
        _audience = configuration["JwtSettings:Audience"];
        var expirationValue = configuration["JwtSettings:ExpirationMinutes"];

        if (string.IsNullOrEmpty(_secret) || string.IsNullOrEmpty(_issuer) || string.IsNullOrEmpty(_audience))
        {
            throw new ArgumentException("JwtSettings:Secret, JwtSettings:Issuer, or JwtSettings:Audience is not properly configured in appsettings.json.");
        }

        if (!int.TryParse(expirationValue, out _expirationMinutes) || _expirationMinutes <= 0)
        {
            throw new ArgumentException("JwtSettings:ExpirationMinutes must be a valid positive integer in appsettings.json.");
        }
    }

    public string GenerateJwtToken(UserEntity user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "User"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secret);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch (Exception ex)
        {
            // Логування або обробка помилки
            Console.WriteLine($"Token validation failed: {ex.Message}");
            return null;
        }
    }
}