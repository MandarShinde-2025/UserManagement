using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserManagement.API.Controllers;
using UserManagement.Application.CQRS.Commands.Orders;
using UserManagement.Application.CQRS.Queries.Orders;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Tests.Controllers.Orders;

public class OrderControllerTests
{
    private readonly Mock<IMediator> _mockMediatR = null!;
    private readonly OrderController _orderController;

    public OrderControllerTests()
    {
        _mockMediatR = new Mock<IMediator>();
        _orderController = new OrderController(_mockMediatR.Object);
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldReturnCreateAtAction()
    {
        // Arrange
        var orderDto = new OrderDto()
        {
            Id = 1,
            OrderDate = DateTime.UtcNow,
            UserId = 1,
            OrderItems = new List<OrderItemDto>()
            {
                new OrderItemDto() { MenuItemId = 1, Quantity = 1, Price = 50 },
                new OrderItemDto() { MenuItemId = 2, Quantity = 1, Price = 50 }
            },
            TotalAmount = 100
        };
        _mockMediatR.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(orderDto.Id);

        // Act
        var result = await _orderController.CreateOrder(new CreateOrderCommand(orderDto));


        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result as CreatedAtActionResult;
        var returnedResult = createdResult!.Value as Order;
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnOk_WhenOrderExists()
    {
        // Arrange
        var orderDto = new OrderDto()
        {
            Id = 1,
            OrderDate = DateTime.UtcNow,
            UserId = 1,
            OrderItems = new List<OrderItemDto>()
            {
                new OrderItemDto() { MenuItemId = 1, Quantity = 1, Price = 50 },
                new OrderItemDto() { MenuItemId = 2, Quantity = 1, Price = 50 }
            },
            TotalAmount = 100
        };
        _mockMediatR.Setup(x => x.Send(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(orderDto);

        // Act
        var okresult = await _orderController.GetOrder(orderDto.Id) as OkObjectResult;
        var returnedOrder = okresult!.Value as OrderDto;

        // Assert
        returnedOrder.Should().NotBeNull();
        returnedOrder.Id.Should().Be(orderDto.Id);
    }

    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnNotFound_WhenOrderDoesNotExists()
    {
        // Arrange
        _mockMediatR.Setup(x => x.Send(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((OrderDto)null!);

        var result = await _orderController.GetOrder(1);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}