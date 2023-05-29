using DataSyncHub.Modules.Users.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DataSyncHub.Modules.Users.Api")]
namespace DataSyncHub.Modules.Users.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<INinjasApiService, NinjasApiService>();
            services.AddSingleton<IUsersRepository, UsersRepository>();
            services.AddSingleton<IUsersCacheService, UsersCacheService>();

            services.AddHostedService<PeriodicDataUploadService>();

            return services;
        }
    }
}