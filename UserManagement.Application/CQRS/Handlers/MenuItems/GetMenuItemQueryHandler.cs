using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Queries.MenuItems;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.MenuItems;

public class GetMenuItemQueryHandler : IRequestHandler<GetMenuItemQuery, MenuItemDto?>
{
    private readonly IMenuItemRepository _repository;
    private readonly IMapper _mapper;

    public GetMenuItemQueryHandler(IMenuItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MenuItemDto?> Handle(GetMenuItemQuery request, CancellationToken cancellationToken)
    {
        var menuItem = await _repository.GetByIdAsync(request.Id);
        if (menuItem is null) return null!;
        return _mapper.Map<MenuItemDto>(menuItem);
    }
}