using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.CQRS.Queries.Orders;

public record GetOrderQuery(int Id): IRequest<OrderDto?>;