using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Interfaces.Auth;
using TaskManagement.Infrastructure.Auth;

namespace TaskManagement.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

        services.AddScoped<IJwtTokeProvider, JwtTokenProvider>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();

        return services;
    }
}