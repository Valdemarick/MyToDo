using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyToDo.WebApi.OptionsSetup;

namespace MyToDo.WebApi;

internal static class DependencyInjection
{
    public static IServiceCollection AddWebApiLayer(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>()
            .ConfigureOptions<JwtBearerOptionsSetup>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        
        services.AddAuthorization();

        return services;
    }
}
