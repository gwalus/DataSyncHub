using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace DataSyncHub.Shared.Infrastructure.Healths
{
    internal class RedisHealthCheck : IHealthCheck
    {
        private readonly IConnectionMultiplexer _connection;

        public RedisHealthCheck(IConnectionMultiplexer connection)
        {
            _connection = connection;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var db = _connection.GetDatabase();

                await db.ExecuteAsync("ping");

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(exception: ex);
            }
        }
    }
}
