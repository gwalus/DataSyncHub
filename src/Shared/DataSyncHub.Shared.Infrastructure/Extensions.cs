using DataSyncHub.Shared.Infrastracture.Api;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using DataSyncHub.Shared.Infrastracture.DAL;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

[assembly: InternalsVisibleTo("DataSyncHub.Bootstrapper")]
namespace DataSyncHub.Shared.Infrastracture
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var configuration = serviceProvider.GetService<IConfiguration>();

            services.AddHttpClient("api-ninjas", client =>
            {
                client.BaseAddress = new Uri(configuration?["ApiNinjas:Url"]);
                client.DefaultRequestHeaders.Add("X-Api-Key", configuration?["ApiNinjas:Token"]);
            });

            services.AddMongoDb(configuration);
            services.AddRedisCache(configuration);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("DataSyncHub API"));
            });

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            return app;
        }

        public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder)
            => hostBuilder.UseSerilog((context, sp, configuration) =>
                {
                    var elasticUri = context.Configuration["ElasticConfiguration:Uri"] 
                        ?? throw new ArgumentException("ElasticConfiguration:Uri configuration cannot be null or empty.");

                    configuration.Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .WriteTo.Console()
                        .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(
                            new Uri(elasticUri))
                        {
                            IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace('.', '-')}-{DateTime.UtcNow:yyyy-MM-ss}",
                            AutoRegisterTemplate = true,
                            NumberOfReplicas = 1,
                            NumberOfShards = 2,
                        })
                        .Enrich.WithEnvironmentName();
                });
    }
}