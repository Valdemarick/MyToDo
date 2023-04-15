using MyToDo.Domain.Abstractions;
using MyToDo.Infrastructure.DateTime;

namespace MyToDo.Web.Configuration.DI;

internal sealed class InfrastructureLayerServiceConfigurator : IServiceConfigurator
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeOffsetProvider, DateTimeOffsetProvider>();
    }
}
