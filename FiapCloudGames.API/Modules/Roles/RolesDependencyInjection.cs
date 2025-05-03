using FiapCloudGames.API.Modules.Roles.Repositories.Implementations;
using FiapCloudGames.API.Modules.Roles.Repositories.Interfaces;
using FiapCloudGames.API.Modules.Roles.Services.Implementations;
using FiapCloudGames.API.Modules.Roles.Services.Interfaces;

namespace FiapCloudGames.API.Modules.Roles
{
    public static class RolesDependencyInjection
    {
        public static IServiceCollection AddRolesServices(this IServiceCollection services)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }
    }
}
