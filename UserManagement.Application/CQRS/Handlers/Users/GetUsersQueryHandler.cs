using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Queries.Users;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Services;

namespace UserManagement.Application.CQRS.Handlers.Users;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly RedisCacheService _cacheService;
    private readonly string key = "all_users";

    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper, RedisCacheService cacheService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var cachedUsers = await _cacheService.GetAsync<List<UserDto>>(key);
        if (cachedUsers is not null) return cachedUsers;

        var users = await _userRepository.GetAllAsync();
        var mappedUsers = users.Select(user => _mapper.Map<UserDto>(user)).ToList();

        await _cacheService.SetAsync(key, mappedUsers, TimeSpan.FromMinutes(10));
        return mappedUsers;
    }
}