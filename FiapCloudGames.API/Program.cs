using FiapCloudGames.API.Database;
using FiapCloudGames.API.DTOs.Requests.GameDTO;
using FiapCloudGames.API.DTOs.Requests.UserDTO;
using FiapCloudGames.API.Middlewares;
using FiapCloudGames.API.Repositories.Implementations;
using FiapCloudGames.API.Repositories.Interfaces;
using FiapCloudGames.API.Services.Configurations.JwtConfigurations;
using FiapCloudGames.API.Services.Handlers;
using FiapCloudGames.API.Services.Implementations;
using FiapCloudGames.API.Services.Interfaces;
using FiapCloudGames.API.Validators.GameValidator;
using FiapCloudGames.API.Validators.UserValidator;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

const string AUTHENTICATION_TYPE = "Bearer";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{ 
    options.AddSecurityDefinition(AUTHENTICATION_TYPE, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. Example: ""Authorization: Bearer.
                          Enter 'Bearer' [space] and then your token in the text input below.
                          Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = AUTHENTICATION_TYPE
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = AUTHENTICATION_TYPE
                    },
                    Scheme = "oauth2",
                    Name = AUTHENTICATION_TYPE,
                    In = ParameterLocation.Header
                },
                new List<string> ()
            }
        });
});

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Host.UseSerilog();

#region Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
#endregion

#region Services
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationResultHandler>();
builder.Services.AddSingleton<IEncryptionService, EncryptionService>();
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IGameService, GameService>();
#endregion

#region Validators
builder.Services.AddScoped<IValidator<RequestCreateUserDTO>, RequestCreateUserValidator>();
builder.Services.AddScoped<IValidator<RequestUpdateUserDTO>, RequestUpdateUserValidator>();
builder.Services.AddScoped<IValidator<RequestCreateGameDTO>, RequestCreateGameValidator>();
builder.Services.AddScoped<IValidator<RequestUpdateGameDTO>, RequestUpdateGameValidator>();
#endregion

builder.Services.AddJwtServices();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseNpgsql(connectionString);
}, ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<UpdateUserMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
