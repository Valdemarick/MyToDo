using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions;
using MyToDo.Persistence;
using MyToDo.Persistence.Repositories;

namespace MyToDo.Web.Configuration.DI;

internal sealed class PersistenceLayerServiceConfigurator : IServiceConfigurator
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
        });

        services.AddScoped<ITaskRepository, TaskRepository>()
            .AddScoped<IMemberRepository, MemberRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
