using Microsoft.Extensions.DependencyInjection;
using Users.Business.Services;

namespace Users.Business;

public static class ServiceExtensions
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
    }
}
