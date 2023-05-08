using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MyToDo.Application.Abstractions.Security;
using MyToDo.Domain.Abstractions;
using MyToDo.Infrastructure.DateTime;
using MyToDo.Infrastructure.Providers;
using MyToDo.Infrastructure.Providers.Abstractions;
using MyToDo.Infrastructure.Security;

namespace MyToDo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, PasswordHasher>()
            .AddTransient<IDateTimeOffsetProvider, DateTimeOffsetProvider>()
            .AddTransient<IJwtProvider, JwtProvider>()
            .AddTransient<IPermissionProvider, PermissionProvider>()
            .AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>()
            .AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }
}
