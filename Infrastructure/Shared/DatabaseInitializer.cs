using Domain.Entities;
using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Helpers;

namespace Infrastructure.Shared;

public class DatabaseInitializer
{
    public static void Initialize(IServiceScope serviceScope)
    {
        _ = new DatabaseInitializer();
        Seed(serviceScope);
    }

    protected async static void Seed(IServiceScope serviceScope)
    {
        var configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();
        var userRepository = serviceScope.ServiceProvider.GetService<IUserRepository>();
        var unitOfWork = serviceScope.ServiceProvider.GetService<IUnitOfWork>();

        var userExists = userRepository.Query();

        if (!userExists.Any() && configuration != null)
        {
            var adminCredentialsConfig = configuration.GetSection("AdminCredentialsConfig");

            var user = new User
            (
                adminCredentialsConfig["Username"],
                HashHelper.HashPassword(adminCredentialsConfig["Password"]),
                adminCredentialsConfig["Email"],
                new UserProfile("Main", "Admin", "12345678911")
            );

            await userRepository.InsertAsync(user);
            await unitOfWork.SaveAsync();
        }
    }
}