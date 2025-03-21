using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.CQRS.Commands.Orders;

public record UpdateOrderCommand(OrderDto Order, string ModifiedBy): IRequest<bool>;