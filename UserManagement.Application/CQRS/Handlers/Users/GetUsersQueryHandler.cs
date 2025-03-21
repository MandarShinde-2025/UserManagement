using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Queries.Users;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.Users;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(user => _mapper.Map<UserDto>(user)).ToList();
    }
}
