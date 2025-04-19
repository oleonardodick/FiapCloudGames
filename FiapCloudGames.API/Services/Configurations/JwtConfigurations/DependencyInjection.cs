using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FiapCloudGames.API.Services.Configurations.JwtConfigurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJwtServices(this IServiceCollection services)
        {
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
