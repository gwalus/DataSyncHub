using DataSyncHub.Modules.Users.Core.Models;
using DataSyncHub.Modules.Users.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataSyncHub.Modules.Users.Api.Controllers
{
    [Route("[controller]")]
    internal class UsersController : ControllerBase
    {
        private static readonly object _lock = new();

        private readonly IUsersRepository _usersRepository;
        private readonly IUsersCacheService _usersCacheService;

        public UsersController(IUsersRepository usersRepository, IUsersCacheService usersCacheService)
        {
            _usersRepository = usersRepository;
            _usersCacheService = usersCacheService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RandomUser>>> GetUsers()
        {
            var cacheData = await _usersCacheService.GetDataAsync<List<RandomUser>>("users");
            if (cacheData != null)
            {
                return Ok(cacheData);
            }

            lock (_lock)
            {
                var expirationTime = DateTimeOffset.Now.AddMinutes(1.0);
                cacheData = _usersRepository.GetAsync().Result;
                _usersCacheService.SetData("users", cacheData, expirationTime);
            }

            return Ok(cacheData);
        }
    }
}
