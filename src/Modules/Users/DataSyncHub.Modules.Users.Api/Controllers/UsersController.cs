using DataSyncHub.Modules.Users.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataSyncHub.Modules.Users.Api.Controllers
{
    [Route("users-module")]
    internal class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
            => Ok(await _usersRepository.GetAsync());
    }
}
