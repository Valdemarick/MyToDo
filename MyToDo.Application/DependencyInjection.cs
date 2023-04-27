using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MyToDo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
        });

        services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly(),
            includeInternalTypes: true);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
