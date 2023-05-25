using DataSyncHub.Modules.Users.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataSyncHub.Modules.Users.Api.Controllers
{
    [Route("users-module")]
    internal class UsersController : ControllerBase
    {
        private readonly INinjasApiService _ninjasApiService;

        public UsersController(INinjasApiService ninjasApiService)
        {
            _ninjasApiService = ninjasApiService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            await _ninjasApiService.GetRandomUserAsync();

            return Ok("asdasda");
        }
    }
}
