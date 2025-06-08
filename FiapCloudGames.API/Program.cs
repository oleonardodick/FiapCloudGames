using FiapCloudGames.API.Database;
using FiapCloudGames.API.Middlewares;
using FiapCloudGames.API.Modules.Auth;
using FiapCloudGames.API.Modules.Encryption;
using FiapCloudGames.API.Modules.Games;
using FiapCloudGames.API.Modules.Roles;
using FiapCloudGames.API.Modules.Users;
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
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Fiap Cloud Games",
        Description = "API ASP.NET Core para o controle de usu�rio e seus jogos adquiridos na plataforma.",
        Contact = new OpenApiContact
        {
            Name = "Leonardo Bernardes",
            Url = new Uri("http://www.linkedin.com/in/oleonardodick")
        }
    });

    options.EnableAnnotations();

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

builder.Services.AddAuthServices();
builder.Services.AddEncryptionServices();
builder.Services.AddUsersServices();
builder.Services.AddGamesServices();
builder.Services.AddRolesServices();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseNpgsql(connectionString);
}, ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(options =>
//     {
//         options.SwaggerEndpoint("/swagger/v1/swagger.json", "Fiap Cloud Games v1");
//     });
// }

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Fiap Cloud Games v1");
    });

app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<UpdateUserMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
