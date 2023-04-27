using Microsoft.Extensions.DependencyInjection;
using MyToDo.Application.Abstractions.Security;
using MyToDo.Infrastructure.Security;

namespace MyToDo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
