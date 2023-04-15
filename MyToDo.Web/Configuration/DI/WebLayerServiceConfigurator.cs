using MyToDo.Web.Middlewares;

namespace MyToDo.Web.Configuration.DI;

internal sealed class WebLayerServiceConfigurator : IServiceConfigurator
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
    }
}
