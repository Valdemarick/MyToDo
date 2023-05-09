using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MyToDo.Application.Abstractions.Security;
using MyToDo.Domain.Abstractions;
using MyToDo.Infrastructure.Providers;
using MyToDo.Infrastructure.Security;
using MyToDo.Infrastructure.Services;
using MyToDo.Infrastructure.Services.Abstractions;

namespace MyToDo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, PasswordHasher>()
            .AddTransient<IDateTimeService, DateTimeService>()
            .AddTransient<IJwtProvider, JwtProvider>()
            .AddTransient<IPermissionService, PermissionService>()
            .AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>()
            .AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }
}
