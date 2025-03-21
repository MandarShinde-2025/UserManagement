using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Commands.MenuItems;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.MenuItems;

public class CreateMenuItemCommandHandler : IRequestHandler<CreateMenuItemCommand, int>
{
    private readonly IMenuItemRepository _repository;
    private readonly IMapper _mapper;

    public CreateMenuItemCommandHandler(IMenuItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menuItem = _mapper.Map<MenuItem>(request.MenuItem);

        await _repository.AddAsync(menuItem);

        return menuItem.Id;
    }
}