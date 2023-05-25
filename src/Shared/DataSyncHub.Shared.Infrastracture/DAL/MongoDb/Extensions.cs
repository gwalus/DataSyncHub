using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DataSyncHub.Shared.Infrastracture.Data.MongoDb
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
    }
}
