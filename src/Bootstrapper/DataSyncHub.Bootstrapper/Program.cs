using DataSyncHub.Bootstrapper;
using DataSyncHub.Shared.Infrastracture;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();

RegisterModules(builder);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Map("/", () => "DataSyncHub API");

app.UseAuthorization();

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