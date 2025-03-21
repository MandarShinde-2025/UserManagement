using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.CQRS.Queries.MenuItems;

public record GetMenuItemQuery(int Id): IRequest<MenuItemDto?>;
