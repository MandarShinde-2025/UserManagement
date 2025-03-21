using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Queries.MenuItems;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.MenuItems;

public class GetMenuItemsQueryHandler : IRequestHandler<GetMenuItemsQuery, IEnumerable<MenuItemDto>>
{
    private readonly IMenuItemRepository _repository;
    private readonly IMapper _mapper;

    public GetMenuItemsQueryHandler(IMenuItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MenuItemDto>> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
    {
        var menuItems = await _repository.GetAllAsync();
        return menuItems.Select(menuItem => _mapper.Map<MenuItemDto>(menuItem)).ToList();
    }
}