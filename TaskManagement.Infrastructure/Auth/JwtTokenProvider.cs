using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Application.Interfaces.Auth;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Auth;

public class JwtTokenProvider(IOptions<JwtOptions> options) : IJwtTokeProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateToken(User user)
    {
        // Create an instance of JwtSecurityTokenHandler
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var key = Encoding.ASCII.GetBytes(_options.SecretKey);

        // Create claims for user identity
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        // Create jwt token
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_options.ExpirationHours),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );
        

        // Write the token as a string and return it
        return tokenHandler.WriteToken(token);
    }
}