using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Persistence.DbContexts;

namespace UserManagement.Tests.Repositories.Orders;

public class OrderRepositoryTests
{
    // Arrange => Act => Assert
    // AAA pattern

    private readonly DbContextOptions<AppDbContext> _options;

    public OrderRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "UserManagement")
            .Options;
    }

    [Fact]
    public async Task AddOrderAsync_ShouldAddOrderToDatabase()
    {
        // Arrange
        using var context = new AppDbContext(_options);
        var repository = new OrdersRepository(context);
        var order = new Order()
        {
            Id = 1,
            OrderDate = DateTime.UtcNow,
            UserId = 1,
            OrderItems = new List<OrderItem>()
            {
                new OrderItem() { Id = 1, MenuItemId = 1, OrderId = 1, Quantity = 1, Price = 50 },
                new OrderItem() { Id = 2, MenuItemId = 2, OrderId = 1, Quantity = 1, Price = 50 }
            },
            TotalAmount = 100
        };
        await repository.AddAsync(order);

        // Act
        var result = await repository.GetByIdAsync(1);
        result.Should().NotBeNull();

        // Assert
        result.UserId.Should().Be(order.UserId);
    }


    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists()
    {
        // Arrange
        using var context = new AppDbContext(_options);
        var repository = new OrdersRepository(context);
        var order = new Order()
        {
            Id = 0,
            OrderDate = DateTime.UtcNow,
            UserId = 1,
            OrderItems = new List<OrderItem>()
            {
                new OrderItem() { Id = 1, MenuItemId = 1, OrderId = 1, Quantity = 1, Price = 50 },
                new OrderItem() { Id = 2, MenuItemId = 2, OrderId = 1, Quantity = 1, Price = 50 }
            },
            TotalAmount = 100
        };
        await repository.AddAsync(order);

        // Act
        var result = await repository.GetByIdAsync(1);
        result.Should().NotBeNull();

        // Assert
        result.Id.Should().Be(1);
    }
}