using FiapCloudGames.API.Modules.Encryption.Services.Implementations;
using FiapCloudGames.API.Modules.Encryption.Services.Interfaces;

namespace FiapCloudGames.API.Modules.Encryption
{
    public static class EncryptionDependencyInjection
    {
        public static IServiceCollection AddEncryptionServices(this IServiceCollection services)
        {
            services.AddSingleton<IEncryptionService, EncryptionService>();

            return services;
        }
    }
}
