using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Persistence.DbContexts;

namespace UserManagement.Tests.Repositories.MenuItems;

public class MenuItemRepositoryTests
{
    // Arrange => Act => Assert
    // AAA pattern

    private readonly DbContextOptions<AppDbContext> _options;

    public MenuItemRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "UserManagement")
            .Options;
    }

    [Fact]
    public async Task AddMenuItemAsync_ShouldAddMenuItemToDatabase()
    {
        // Arrange
        using var context = new AppDbContext(_options);
        var repository = new MenuItemsRepository(context);
        var menuItem = new MenuItem()
        {
            Id = 0,
            Name = "Poha",
            Description = "Breakfast",
            Price = 25
        };
        await repository.AddAsync(menuItem);

        // Act
        var result = await repository.GetByIdAsync(1);
        result.Should().NotBeNull();

        // Assert
        result.Name.Should().Be("Poha");
    }


    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnMenuItem_WhenMenuItemExists()
    {
        // Arrange
        using var context = new AppDbContext(_options);
        var repository = new MenuItemsRepository(context);
        var menuItem = new MenuItem()
        {
            Id = 0,
            Name = "Idli",
            Description = "Breakfast",
            Price = 30
        };
        await repository.AddAsync(menuItem);

        // Act
        var result = await repository.GetByIdAsync(1);
        result.Should().NotBeNull();

        // Assert
        result.Name.Should().Be("Poha");
    }
}