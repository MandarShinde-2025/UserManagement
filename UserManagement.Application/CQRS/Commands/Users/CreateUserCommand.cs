using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.CQRS.Commands.Users;
public record CreateUserCommand(CreateUserDto User): IRequest<int>;