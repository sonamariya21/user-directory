using Microsoft.Extensions.DependencyInjection;
using UserDirectory.Application.Users;

namespace UserDirectory.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}
