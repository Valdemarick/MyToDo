using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyToDo.Domain.Abstractions;
using MyToDo.Persistence.Repositories;

namespace MyToDo.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
        });

        services.AddTransient<ITaskRepository, TaskRepository>()
            .AddTransient<IMemberRepository, MemberRepository>()
            .AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
