using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.CQRS.Commands.Orders;

public record CreateOrderCommand(OrderDto Order): IRequest<int>;