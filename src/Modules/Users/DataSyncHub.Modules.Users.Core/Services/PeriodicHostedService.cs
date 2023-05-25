using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataSyncHub.Modules.Users.Core.Services
{
    internal class PeriodicHostedService : BackgroundService
    {
        private readonly ILogger<PeriodicHostedService> _logger;
        private readonly INinjasApiService _ninjasApiService;
        private readonly IUsersRepository _usersRepository;
        private readonly TimeSpan _period = TimeSpan.FromMinutes(1);

        public PeriodicHostedService(ILogger<PeriodicHostedService> logger, INinjasApiService ninjasApiService, IUsersRepository usersRepository)
        {
            _logger = logger;
            _ninjasApiService = ninjasApiService;
            _usersRepository = usersRepository;
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

                    await _usersRepository.CreateAsync(randomUser);

                    _logger.LogInformation($"I retrieved user called: {randomUser.Name}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        $"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
                }
            }
        }
    }
}
