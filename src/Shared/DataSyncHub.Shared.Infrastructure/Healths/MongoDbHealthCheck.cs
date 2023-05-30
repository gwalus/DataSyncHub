using DataSyncHub.Shared.Infrastracture.Data.MongoDb;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataSyncHub.Shared.Infrastructure.Healths
{
    internal class MongoDbHealthCheck : IHealthCheck
    {
        private readonly IMongoClient _client;
        private readonly IOptions<MongoDbOptions> _options;

        public MongoDbHealthCheck(IMongoClient client, IOptions<MongoDbOptions> options)
        {
            _client = client;
            _options = options;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var db = _client.GetDatabase(_options.Value.DatabaseName);

                await db.RunCommandAsync((Command<BsonDocument>)"{ping:1}");

                return HealthCheckResult.Healthy();
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}
