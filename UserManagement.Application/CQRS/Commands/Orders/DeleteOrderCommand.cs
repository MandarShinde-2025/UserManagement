using MediatR;

namespace UserManagement.Application.CQRS.Commands.Orders;

public record DeleteOrderCommand(int Id): IRequest<bool>;