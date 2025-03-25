using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserManagement.API.Controllers;
using UserManagement.Application.CQRS.Commands.Users;
using UserManagement.Application.CQRS.Queries.Users;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Tests.Controllers.Users;

public class UserControllerTests
{
    private readonly Mock<IMediator> _mockMediatR = null!;
    private readonly UserController _userController;

    public UserControllerTests()
    {
        _mockMediatR = new Mock<IMediator>();
        _userController = new UserController(_mockMediatR.Object);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnCreateAtAction()
    {
        // Arrange
        var userDto = new CreateUserDto() { Id = 1, Name = "Mandar Shinde", Email = "mandar@test.com", Role = "Customer" };
        _mockMediatR.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(userDto.Id);

        // Act
        var result = await _userController.CreateUser(new CreateUserCommand(userDto));


        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result as CreatedAtActionResult;
        var returnedResult = createdResult!.Value as User;
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnOk_WhenUserExists()
    {
        // Arrange
        var userDto = new UserDto() { Id = 1, Name = "Mandar Shinde", Email = "mandar@test.com", Role = "Customer" };
        _mockMediatR.Setup(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(userDto);

        // Act
        var okresult = await _userController.GetUser(1) as OkObjectResult;
        var returnedUser = okresult!.Value as UserDto;

        // Assert
        returnedUser.Should().NotBeNull();
        returnedUser.Name.Should().Be(userDto.Name);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnNoContent_WhenUserDoesNotExists()
    {
        // Arrange
        _mockMediatR.Setup(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((UserDto)null!);

        var result = await _userController.GetUser(1);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}