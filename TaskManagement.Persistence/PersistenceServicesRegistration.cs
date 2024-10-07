using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services) => services;
}