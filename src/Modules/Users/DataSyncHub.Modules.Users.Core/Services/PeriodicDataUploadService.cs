using Microsoft.Extensions.Hosting;

namespace DataSyncHub.Modules.Users.Core.Services
{
    internal class PeriodicDataUploadService : BackgroundService
    {
        private readonly Serilog.ILogger _logger;
        private readonly INinjasApiService _ninjasApiService;
        private readonly IUsersRepository _usersRepository;
        private readonly TimeSpan _period = TimeSpan.FromMinutes(1);

        public PeriodicDataUploadService(Serilog.ILogger logger, INinjasApiService ninjasApiService, IUsersRepository usersRepository)
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

                    _logger.Information($"New random user has been saved: {randomUser.Name}");
                }
                catch (Exception ex)
                {
                    _logger.Error(
                        $"Something went wrong. Inner message: {ex.Message}");
                }
            }
        }
    }
}
