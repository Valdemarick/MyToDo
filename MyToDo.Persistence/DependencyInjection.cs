using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
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

        services.AddScoped<ITaskRepository, TaskRepository>()
            .AddScoped<IMemberRepository, MemberRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ITagRepository, TagRepository>()
            .AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}
