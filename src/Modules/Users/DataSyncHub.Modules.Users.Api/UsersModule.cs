using DataSyncHub.Modules.Users.Core;
using DataSyncHub.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DataSyncHub.Modules.Users.Api
{
    internal class UsersModule : IModule
    {
        public string Name => "Users";
        public string Path => "users-module";

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder applicationBuilder)
        {

        }
    }
}