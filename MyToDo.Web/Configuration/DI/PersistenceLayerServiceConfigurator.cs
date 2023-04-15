using Microsoft.EntityFrameworkCore;
using MyToDo.Persistence;

namespace MyToDo.Web.Configuration.DI;

internal sealed class PersistenceLayerServiceConfigurator : IServiceConfigurator
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
        });
    }
}
