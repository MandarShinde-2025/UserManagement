using MediatR;
using UserManagement.Application.CQRS.Commands.Users;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.Users;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null) return false;

        return await _userRepository.DeleteAsync(request.Id);
    }
}
