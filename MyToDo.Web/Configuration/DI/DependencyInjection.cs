using System.Reflection;
using MyToDo.Domain.Abstractions;
using MyToDo.Infrastructure.DateTime;
using MyToDo.Persistence;
using MyToDo.Persistence.Repositories;

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

        services.AddScoped<ITaskRepository, TaskRepository>()
            .AddScoped<IMemberRepository, MemberRepository>()
            .AddScoped<IDateTimeOffsetProvider, DateTimeOffsetProvider>()
            .AddScoped<IUnitOfWork, UnitOfWork>(); // trouble with IBaseSpecification. Scrutor also tries to register it.
        
        return services;

        static bool IsAssignedToType<T>(TypeInfo typeInfo) =>
            typeof(T).IsAssignableFrom(typeInfo) &&
            typeInfo is { IsInterface: false, IsAbstract: false };
    }

    // private static IServiceCollection ResolveDependencies(this IServiceCollection services)
    // {
    //     return services.Scan(scan =>
    //         scan.FromAssemblies(
    //                 Infrastructure.AssemblyReference.Assembly,
    //                 Persistence.AssemblyReference.Assembly,
    //                 Web.AssemblyReference.Assembly)
    //             .AddClasses(classes => classes.Where(c => c != typeof(IBaseSpecification<>)), false)
    //             .AsImplementedInterfaces(t => t != typeof(IBaseSpecification<>))
    //             .WithScopedLifetime());
    // }
}
