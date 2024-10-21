using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Behaviors;
using TaskManagement.Application.Features.Task.Commands.Create;
using TaskManagement.Application.Features.Task.Commands.Update;
using TaskManagement.Application.Features.User.Commands.Login;
using TaskManagement.Application.Features.User.Commands.Register;

namespace TaskManagement.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // Register FluentValidation Validators
        services.AddValidatorsFromAssemblyContaining<LoginUserCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateTaskCommand>();
        services.AddValidatorsFromAssemblyContaining<UpdateTaskCommand>();
        
        // Add pipeline behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}