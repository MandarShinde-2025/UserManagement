using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using UserManagement.Application.CQRS.Commands.Orders;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Application.Mappings;
using FluentValidation.AspNetCore;

namespace UserManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registering MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

        // Registering FluentValidation Assembly
        services.AddValidatorsFromAssembly(typeof(CreateOrderCommand).Assembly);
        services.AddFluentValidationAutoValidation();

        // Registering Automapper
        services.AddAutoMapper(typeof(OrderMappingProfile).Assembly);

        services.AddScoped<IMenuItemRepository, MenuItemsRepository>();
        services.AddScoped<IOrderRepository, OrdersRepository>();
        services.AddScoped<IUserRepository, UsersRepository>();

        return services;
    }
}