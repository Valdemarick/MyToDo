using Microsoft.Extensions.DependencyInjection;
using MyToDo.Shared.Middlewares;

namespace MyToDo.Shared.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddGlobalExceptionHandling(this IServiceCollection services)
    {
        return services.AddScoped<GlobalExceptionHandlingMiddleware>();
    }
}
