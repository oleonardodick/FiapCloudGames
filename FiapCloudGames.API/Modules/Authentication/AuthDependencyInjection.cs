using FiapCloudGames.API.Modules.Authentication.Configurations.Implementations;
using FiapCloudGames.API.Modules.Authentication.Configurations.Interfaces;
using FiapCloudGames.API.Modules.Authentication.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FiapCloudGames.API.Modules.Authentication.Handlers;
using Microsoft.AspNetCore.Authorization;
using FiapCloudGames.API.Modules.Authentication.Services.Implementations;
using FiapCloudGames.API.Modules.Authentication.Services.Interfaces;

namespace FiapCloudGames.API.Modules.Auth
{
    public static class AuthDependencyInjection
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationResultHandler>();

            services.AddSingleton<IJwtService, JwtService>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddSingleton<IJwtKeyProvider, JwtKeyProvider>();

            services.AddSingleton<JwtAuthenticationConfigurator>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   // O JwtAuthenticationConfigurator é injetado diretamente pelo ASP.NET Core DI
                   services.BuildServiceProvider()
                       .GetRequiredService<JwtAuthenticationConfigurator>()
                       .ConfigureJwtBearerOptions(options);
               });

            return services;
        }
    }
}
