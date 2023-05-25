using DataSyncHub.Modules.Users.Core.Models;

namespace DataSyncHub.Modules.Users.Core.Services
{
    internal interface INinjasApiService
    {
        Task<RandomUser> GetRandomUserAsync();
    }
}
