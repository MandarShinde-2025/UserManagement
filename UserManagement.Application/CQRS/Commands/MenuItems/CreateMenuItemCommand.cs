using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.CQRS.Commands.MenuItems;

public record CreateMenuItemCommand(MenuItemDto MenuItem) : IRequest<int>;