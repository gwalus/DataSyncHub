using DataSyncHub.Shared.Infrastracture.Api;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using DataSyncHub.Shared.Infrastracture.Data.MongoDb;
using MongoDB.Driver;

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
                client.BaseAddress = new Uri("https://api.api-ninjas.com/v1/");
                client.DefaultRequestHeaders.Add("X-Api-Key", configuration?["Secrets:ApiNinjasToken"]);
            });

            services.Configure<MongoDbOptions>(configuration.GetSection("MongoDB"));
            services.AddSingleton<IMongoClient>(client =>
            {
                return new MongoClient(configuration["MongoDB:ConnectionURI"]);
            });

            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });            

            return services;
        }
    }
}