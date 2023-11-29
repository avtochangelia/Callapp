using Application.Shared.Behaviors;
using Application.Shared.Configurations;
using Application.Shared.Configurations.Validators;
using Application.Shared.Exceptions;
using Application.Shared.Options;
using Application.UserManagement.Commands.CreateUser;
using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Infrastructure.DataAccess;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Reflection;
using System.Text;

namespace DI;

public class DependencyResolver
{
    public IConfiguration Configuration { get; }

    public DependencyResolver(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IServiceCollection Resolve(IServiceCollection services)
    {
        if (services == null)
        {
            services = new ServiceCollection();
        }

        var appsettings = new AppSettings();
        Configuration.Bind(appsettings);
        ValidateConfiguration(appsettings);

        services.AddDbContext<EFDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Default")));

        services.AddScoped<HttpClient>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.Configure<TypicodeConfig>(Configuration.GetSection("TypicodeConfig"));

        services.AddMediatR(new[]
        {
            typeof(CreateUser).GetTypeInfo().Assembly,
        });

        services.AddValidatorsFromAssembly(typeof(AppSettingsValidator).GetTypeInfo().Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        ConfgiureJwt(services, Configuration);

        return services;
    }

    public static void ConfgiureJwt(IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(JwtOptions.ConfigSection).Get<JwtOptions>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Audience = jwtOptions!.ValidAudience;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.ValidIssuer,
                ValidAudiences = jwtOptions.ValidAudiences,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
            };
        });
    }

    internal static void ValidateConfiguration(AppSettings appSettings)
    {
        var validator = new AppSettingsValidator();
        var validationResult = validator.Validate(appSettings);
        if (!validationResult.IsValid)
        {
            throw new MissingAppsettingsException(validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
        }
    }
}