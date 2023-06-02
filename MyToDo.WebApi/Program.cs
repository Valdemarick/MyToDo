using MyToDo.Application;
using MyToDo.Infrastructure;
using MyToDo.Persistence;
using MyToDo.Shared.Extensions;
using MyToDo.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDomainLayer()
    .AddApplicationLayer()
    .AddPersistenceLayer(builder.Configuration)
    .AddInfrastructureLayer()
    .AddWebApiLayer();

builder.Services.AddGlobalExceptionHandling();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandlingMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
