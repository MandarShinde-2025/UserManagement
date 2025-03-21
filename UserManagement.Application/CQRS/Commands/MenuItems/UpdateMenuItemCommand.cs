using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.CQRS.Commands.MenuItems;

public record UpdateMenuItemCommand(MenuItemDto MenuItem) : IRequest<bool>;