namespace MyToDo.Web.Configuration.DI;

internal interface IServiceConfigurator
{
    void Configure(IServiceCollection services, IConfiguration configuration);
}
