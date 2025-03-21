using MediatR;

namespace UserManagement.Application.CQRS.Commands.Users;

public record DeleteUserCommand(int Id): IRequest<bool>;
