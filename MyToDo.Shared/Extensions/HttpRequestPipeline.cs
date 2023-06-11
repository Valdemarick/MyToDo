using Microsoft.AspNetCore.Builder;
using MyToDo.Shared.Middlewares;

namespace MyToDo.Shared.Extensions;

public static class HttpRequestPipeline
{
    public static IApplicationBuilder UseGlobalExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
