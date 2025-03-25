using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Queries.Users;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Services;

namespace UserManagement.Application.CQRS.Handlers.Users;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly RedisCacheService _cacheService;
    private string key = "user_";

    public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper, RedisCacheService cacheService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var cachedUser = await _cacheService.GetAsync<UserDto>(key + request.Id);
        if (cachedUser is not null) return cachedUser;

        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null) return null!;
        var mappedUser = _mapper.Map<UserDto>(user);

        await _cacheService.SetAsync(key + request.Id, mappedUser, TimeSpan.FromMinutes(10));
        return mappedUser;
    }
}