using System.Reflection.Metadata.Ecma335;

namespace TaskManagement.Infrastructure.Auth;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public int ExpirationHours { get; set; }
}