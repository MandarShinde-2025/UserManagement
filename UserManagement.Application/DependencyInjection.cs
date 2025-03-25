using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using UserManagement.Application.CQRS.Commands.Orders;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Application.Mappings;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;

namespace UserManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _configuration)
    {
        // Registering MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

        // Registering FluentValidation Assembly
        services.AddValidatorsFromAssembly(typeof(CreateOrderCommand).Assembly);
        services.AddFluentValidationAutoValidation();

        // Registering Automapper
        services.AddAutoMapper(typeof(OrderMappingProfile).Assembly);

        // Registering redis cache server
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = _configuration.GetSection("Redis:DefaultConnection")!.Value;
            //options.Configuration = "127.0.0.1:6379";
            options.InstanceName = "UserManagement_";
        });

        return services;
    }
}