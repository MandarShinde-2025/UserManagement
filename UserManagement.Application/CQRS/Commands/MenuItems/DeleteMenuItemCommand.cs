using MediatR;

namespace UserManagement.Application.CQRS.Commands.MenuItems;

public record DeleteMenuItemCommand(int Id): IRequest<bool>;
