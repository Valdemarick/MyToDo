﻿using System.Reflection;

namespace MyToDo.Web.Configuration.DI;

internal static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies)
    {
        var serviceConfigurators = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(IsAssignedToType<IServiceConfigurator>)
            .Select(Activator.CreateInstance)
            .Cast<IServiceConfigurator>();
        
        foreach (var serviceConfigurator in serviceConfigurators)
        {
            serviceConfigurator.Configure(services, configuration);
        }

        return services;

        static bool IsAssignedToType<T>(TypeInfo typeInfo) =>
            typeof(T).IsAssignableFrom(typeInfo) &&
            typeInfo is { IsInterface: false, IsAbstract: false };
    }
}
