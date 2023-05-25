using StackExchange.Redis;
using System.Text.Json;

namespace DataSyncHub.Modules.Users.Core.Services
{
    internal class UsersCacheService : IUsersCacheService
    {
        private readonly IDatabase _database;

        public UsersCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<T> GetDataAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        public object RemoveData(string key)
        {
            bool _isKeyExist = _database.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _database.KeyDelete(key);
            }
            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _database.StringSet(key, JsonSerializer.Serialize(value), expiryTime);
            return isSet;
        }
    }
}
