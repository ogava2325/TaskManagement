using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        var claims = new List<Claim>
        {
            new Claim("userId", user.Id.ToString()),
        };

        // Create an identity from the claims
        var identity = new ClaimsIdentity(claims);

        // Describe the token settings
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            Subject = identity,
            NotBefore = DateTime.UtcNow, // Ensure NotBefore is set to now
            Expires = DateTime.UtcNow.AddHours(_options.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        // Create a JWT security token
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        // Write the token as a string and return it
        return tokenHandler.WriteToken(token);
    }
}