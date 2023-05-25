using DataSyncHub.Modules.Users.Core.Models;
using DataSyncHub.Modules.Users.Core.Models.MongoDb;

namespace DataSyncHub.Modules.Users.Core.Mappers
{
    internal static class UserMapper
    {
        public static List<RandomUser> MapToRandomUsers(List<User> users)
            => users.ConvertAll(user => new RandomUser
            {
                Address = user.Address,
                Birthday = user.Birthday,
                Email = user.Email,
                Name = user.Name,
                Sex = user.Sex,
                Username = user.Username
            });
    }
}
