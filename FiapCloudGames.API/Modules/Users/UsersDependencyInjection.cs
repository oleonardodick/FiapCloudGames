using FiapCloudGames.API.Modules.Users.DTOs.Requests;
using FiapCloudGames.API.Modules.Users.Repositories.Implementations;
using FiapCloudGames.API.Modules.Users.Repositories.Interfaces;
using FiapCloudGames.API.Modules.Users.Services.Implementations;
using FiapCloudGames.API.Modules.Users.Services.Interfaces;
using FiapCloudGames.API.Modules.Users.Validators;
using FluentValidation;

namespace FiapCloudGames.API.Modules.Users
{
    public static class UsersDependencyInjection
    {
        public static IServiceCollection AddUsersServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IValidator<RequestCreateUserDTO>, RequestCreateUserValidator>();
            services.AddScoped<IValidator<RequestUpdateUserDTO>, RequestUpdateUserValidator>();

            return services;
        }
    }
}
