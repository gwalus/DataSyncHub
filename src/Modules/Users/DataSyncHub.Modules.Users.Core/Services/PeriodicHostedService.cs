using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataSyncHub.Modules.Users.Core.Services
{
    internal class PeriodicHostedService : BackgroundService
    {
        private readonly ILogger<PeriodicHostedService> _logger;
        private readonly INinjasApiService _ninjasApiService;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(10);

        public PeriodicHostedService(ILogger<PeriodicHostedService> logger, INinjasApiService ninjasApiService)
        {
            _logger = logger;
            _ninjasApiService = ninjasApiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new(_period);
            
            while (!stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    var randomUser = await _ninjasApiService.GetRandomUserAsync();

                    _logger.LogInformation($"I retrieved user called: {randomUser.Name}");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        $"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
                }
            }
        }
    }
}
