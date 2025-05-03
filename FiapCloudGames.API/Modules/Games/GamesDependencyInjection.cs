using FiapCloudGames.API.Modules.Games.DTOs.Requests;
using FiapCloudGames.API.Modules.Games.Repositories.Implementations;
using FiapCloudGames.API.Modules.Games.Repositories.Interfaces;
using FiapCloudGames.API.Modules.Games.Services.Implementations;
using FiapCloudGames.API.Modules.Games.Services.Interfaces;
using FiapCloudGames.API.Modules.Games.Validators;
using FluentValidation;

namespace FiapCloudGames.API.Modules.Games
{
    public static class GamesDependencyInjection
    {
        public static IServiceCollection AddGamesServices(this IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IValidator<RequestCreateGameDTO>, RequestCreateGameValidator>();
            services.AddScoped<IValidator<RequestUpdateGameDTO>, RequestUpdateGameValidator>();

            return services;
        }
    }
}
