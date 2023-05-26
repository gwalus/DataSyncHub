using DataSyncHub.Shared.Infrastracture.Data.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StackExchange.Redis;

namespace DataSyncHub.Shared.Infrastracture.DAL
{
    internal static class Extensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
            => services
                .Configure<MongoDbOptions>(configuration.GetSection("MongoDB"))
                .AddSingleton<IMongoClient>(client =>
                {
                    return new MongoClient(configuration["MongoDB:ConnectionURI"]);
                });

        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
            => services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(new ConfigurationOptions
                {
                    EndPoints = { $"{configuration["Redis:Host"]}:{configuration["Redis:Port"]}" },
                    AbortOnConnectFail = false,
                }));
    }
}