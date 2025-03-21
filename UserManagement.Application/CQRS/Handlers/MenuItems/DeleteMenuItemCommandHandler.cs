using MediatR;
using UserManagement.Application.CQRS.Commands.MenuItems;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.MenuItems;

public class DeleteMenuItemCommandHandler : IRequestHandler<DeleteMenuItemCommand, bool>
{

    private readonly IMenuItemRepository _menuItemRepository;

    public DeleteMenuItemCommandHandler(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public async Task<bool> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(request.Id);
        if (menuItem is null) return false;

        return await _menuItemRepository.DeleteAsync(request.Id);
    }
}
