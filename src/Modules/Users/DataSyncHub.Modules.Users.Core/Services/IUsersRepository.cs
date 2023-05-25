using DataSyncHub.Modules.Users.Core.Models;

namespace DataSyncHub.Modules.Users.Core.Services
{
    internal interface IUsersRepository
    {
        Task<List<RandomUser>> GetAsync();
        Task CreateAsync(RandomUser user);
        Task Update(RandomUser user);
        Task DeleteAsync(string id);
    }
}
