using DataSyncHub.Shared.Infrastracture.Api;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using DataSyncHub.Shared.Infrastracture.DAL;

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

            services.AddMongoDb(configuration);
            services.AddRedisCache(configuration);

            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });

            return services;
        }
    }
}