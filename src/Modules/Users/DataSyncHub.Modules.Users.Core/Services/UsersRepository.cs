using DataSyncHub.Modules.Users.Core.Mappers;
using DataSyncHub.Modules.Users.Core.Models;
using DataSyncHub.Modules.Users.Core.Models.MongoDb;
using DataSyncHub.Shared.Infrastracture.Data.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataSyncHub.Modules.Users.Core.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IUsersCacheService _usersCacheService;

        public UsersRepository(IOptions<MongoDbOptions> options, IMongoClient mongoClient, IUsersCacheService usersCacheService)
        {
            var database = mongoClient.GetDatabase(options.Value.DatabaseName);
            _usersCollection = database.GetCollection<User>(options.Value.CollectionName);
            _usersCacheService = usersCacheService;
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(RandomUser userDto)
        {
            var user = new User
            {
                Address = userDto.Address,
                Birthday = userDto.Birthday,
                Email = userDto.Email,
                Name = userDto.Name,
                Sex = userDto.Sex,
                Username = userDto.Username
            };

            await _usersCollection.InsertOneAsync(user);
            _usersCacheService.RemoveData("users");
        }

        public async Task<List<RandomUser>> GetAsync()
        {
            var users = await _usersCollection.Find(new BsonDocument()).ToListAsync();

            return UserMapper.MapToRandomUsers(users);
        }

        public Task Update(RandomUser user)
        {
            throw new NotImplementedException();
        }
    }
}
