using Microsoft.Extensions.DependencyInjection;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Infrastructure.Services;

namespace UserManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Regestering Repositories
        services.AddScoped<IMenuItemRepository, MenuItemsRepository>();
        services.AddScoped<IOrderRepository, OrdersRepository>();
        services.AddScoped<IUserRepository, UsersRepository>();

        // Registering Redis Cache Service
        services.AddScoped<RedisCacheService>();

        return services;
    }
}