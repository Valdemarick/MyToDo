using Microsoft.Extensions.DependencyInjection;
using MyToDo.Application.Abstractions.Security;
using MyToDo.Domain.Abstractions;
using MyToDo.Infrastructure.DateTime;
using MyToDo.Infrastructure.Security;

namespace MyToDo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, PasswordHasher>()
            .AddTransient<IDateTimeOffsetProvider, DateTimeOffsetProvider>();

        return services;
    }
}
