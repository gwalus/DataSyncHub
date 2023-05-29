using DataSyncHub.Bootstrapper;
using DataSyncHub.Shared.Infrastracture;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();

RegisterModules(builder);

builder.Host.ConfigureSerilog();

var app = builder.Build();

app.UseInfrastructure();

app.Run();

void RegisterModules(WebApplicationBuilder builder)
{
    var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration);
    var modules = ModuleLoader.LoadModules(assemblies);

    foreach (var module in modules)
    {
        module.Register(builder.Services);
    }
}