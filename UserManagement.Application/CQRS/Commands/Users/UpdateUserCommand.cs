using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.CQRS.Commands.Users;

public record UpdateUserCommand(UserDto User): IRequest<bool>;