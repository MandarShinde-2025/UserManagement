using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Persistence.DbContexts;

namespace UserManagement.Tests.Repositories.Users;

public class UserRepositoryTests
{
    // Arrange => Act => Assert
    // AAA pattern

    private readonly DbContextOptions<AppDbContext> _options;

    public UserRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "UserManagement")
            .Options;
    }

    [Fact]
    public async Task AddUserAsync_ShouldAddUserToDatabase()
    {
        // Arrange
        using var context = new AppDbContext(_options);
        var repository = new UsersRepository(context);
        var user = new User() { Id = 1, Name = "Mandar Shinde", Email = "mandar@test.com", Role = "Customer" };
        await repository.AddAsync(user);

        // Act
        var result = await repository.GetByIdAsync(1);
        result.Should().NotBeNull();

        // Assert
        result.Name.Should().Be(user.Name);
    }


    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        using var context = new AppDbContext(_options);
        
        var user = new User() { Id = 2, Name = "Mayur Shinde", Email = "mayur@test.com", Role = "Chef" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var repository = new UsersRepository(context);
        var result = await repository.GetByIdAsync(2);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
    }

    [Fact]
    public async Task DeleteUserAsync_ShouldRemoveUserFromDatabase()
    {
        // Arrange
        using var context = new AppDbContext(_options);

        var user = new User() { Id = 3, Name = "Rahul Shinde", Email = "rahul@test.com", Role = "Customer" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var repository = new UsersRepository(context);
        await repository.DeleteAsync(user.Id);

        var result = await repository.GetByIdAsync(user.Id);

        // Assert
        result.Should().BeNull();
    }
}