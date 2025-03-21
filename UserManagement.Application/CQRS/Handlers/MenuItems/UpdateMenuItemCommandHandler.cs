using MediatR;
using UserManagement.Application.CQRS.Commands.MenuItems;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.MenuItems;

public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand, bool>
{
    private readonly IMenuItemRepository _repository;

    public UpdateMenuItemCommandHandler(IMenuItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menuItem = await _repository.GetByIdAsync(request.MenuItem.Id)!;
        if (menuItem is null) return false;

        menuItem.Name = request.MenuItem.Name;
        menuItem.Price = request.MenuItem.Price;
        menuItem.Description = request.MenuItem.Description;

        return await _repository.UpdateAsync(menuItem);
    }
}