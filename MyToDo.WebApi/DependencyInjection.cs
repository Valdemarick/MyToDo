﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
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

        services.ConfigureSwagger();

        return services;
    }

    private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyToDo Api", Version = "v1"});
    
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter a JWT token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
    
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}
