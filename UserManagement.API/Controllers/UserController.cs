using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.CQRS.Commands.Users;
using UserManagement.Application.CQRS.Queries.Users;

namespace UserManagement.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(users);
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _mediator.Send(new GetUserQuery(id));
            return user is null ? NoContent() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUser), new { id = userId }, userId);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.User.Id) return BadRequest();

            var result = await _mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));

            return result ? NoContent() : NotFound();
        }
    }
}