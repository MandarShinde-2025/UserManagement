using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserManagement.API.Controllers;
using UserManagement.Application.CQRS.Commands.MenuItems;
using UserManagement.Application.CQRS.Queries.MenuItems;
using UserManagement.Application.DTOs;

namespace UserManagement.Tests.Controllers.MenuItems;

public class MenuItemControllerTests
{
    private readonly Mock<IMediator> _mockMediatR = null!;
    private readonly MenuItemsController _menuItemController = null!;

    public MenuItemControllerTests()
    {
        _mockMediatR = new Mock<IMediator>();
        _menuItemController = new MenuItemsController(_mockMediatR.Object);
    }

    [Fact]
    public async Task CreateMenuItemAsync_ShouldReturnCreateAtAction()
    {
        // Arrange
        var menuItemDto = new MenuItemDto()
        {
            Id = 0,
            Name = "Poha",
            Description = "Breakfast",
            Price = 25
        };
        _mockMediatR.Setup(x => x.Send(It.IsAny<CreateMenuItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(menuItemDto.Id);

        // Act
        var result = await _menuItemController.CreateMenuItem(new CreateMenuItemCommand(menuItemDto));

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result as CreatedAtActionResult;
        var returnedResult = createdResult!.Value as MenuItemDto;
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetMenuItemByIdAsync_ShouldReturnOk_WhenMenuItemExists()
    {
        // Arrange
        var menuItemDto = new MenuItemDto()
        {
            Id = 0,
            Name = "Poha",
            Description = "Breakfast",
            Price = 25
        };
        _mockMediatR.Setup(x => x.Send(It.IsAny<GetMenuItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(menuItemDto);

        // Act
        var okresult = await _menuItemController.GetMenuItem(1) as OkObjectResult;
        var returnedMenuItem = okresult!.Value as MenuItemDto;

        // Assert
        returnedMenuItem.Should().NotBeNull();
        returnedMenuItem.Id.Should().Be(0);
    }

    [Fact]
    public async Task GetMenuItemByIdAsync_ShouldReturnNotFound_WhenMenuItemDoesNotExists()
    {
        // Arrange
        _mockMediatR.Setup(x => x.Send(It.IsAny<GetMenuItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((MenuItemDto)null!);

        var result = await _menuItemController.GetMenuItem(1);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}