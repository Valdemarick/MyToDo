using MyToDo.Application;
using MyToDo.Infrastructure;
using MyToDo.Persistence;
using MyToDo.Web.Middlewares;
using MyToDo.Web.Options;
using MyToDo.Web.Services;
using MyToDo.Web.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddApplicationLayer()
    .AddPersistenceLayer(builder.Configuration)
    .AddInfrastructureLayer();

builder.Services.AddScoped<GlobalExceptionHandlingMiddleware>();

builder.Services.AddScoped<ITaskService, TaskService>()
    .AddScoped<IMemberService, MemberService>()
    .AddScoped<ITagService, TagService>();

builder.Services.Configure<MyToDoServerOptions>(options =>
    builder.Configuration.GetSection("MyToDoServerClient").Bind(options));

builder.Services.AddHttpClient(builder.Configuration["MyToDoServerClient:Name"]!, client =>
    client.BaseAddress = new Uri($"https://{builder.Configuration["MyToDoServerClient:Host"]}:{builder.Configuration["MyToDoServerClient:Port"]}"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
