using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace MyToDo.Web.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Server error",
                Detail = "An internal server error occurred",
            };

            var json = JsonSerializer.Serialize(problemDetails);

            await context.Response.WriteAsync(json);

            context.Response.ContentType = "application/json";
        }
    }
}
