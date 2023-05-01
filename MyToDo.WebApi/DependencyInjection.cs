using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyToDo.WebApi.OptionsSetup;

namespace MyToDo.WebApi;

internal static class DependencyInjection
{
    public static IServiceCollection AddWebApiLayer(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        
        services.ConfigureOptions<JwtOptionsSetup>()
            .ConfigureOptions<JwtBearerOptionsSetup>();

        return services;
    }
}
