using DataSyncHub.Bootstrapper;
using DataSyncHub.Shared.Infrastracture;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();

RegisterModules(builder);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, sp, configuration) =>
{
    configuration.Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(
            new Uri(context.Configuration["ElasticConfiguration:Uri"]))
        {
            IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace('.', '-')}-{DateTime.UtcNow:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfReplicas = 1,
            NumberOfShards = 2,
        })
        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
            .ReadFrom.Configuration(context.Configuration);

        //.Enrich.WithEnvironmentName();
});

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