using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.CQRS.Queries.Users;

public record GetUserQuery(int Id): IRequest<UserDto>;