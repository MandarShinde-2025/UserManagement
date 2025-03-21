using MediatR;
using UserManagement.Application.CQRS.Commands.Users;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.Users;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.User.Id)!;

        if (user is null) return false;

        user.Name = request.User.Name;
        user.Email = request.User.Email;
        user.Role = request.User.Role;

        return await _userRepository.UpdateAsync(user);
    }
}