using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.CQRS.Queries.MenuItems;

public record GetMenuItemsQuery() : IRequest<IEnumerable<MenuItemDto>>;