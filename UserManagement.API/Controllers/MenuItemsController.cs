using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.CQRS.Commands.MenuItems;
using UserManagement.Application.CQRS.Queries.MenuItems;

namespace UserManagement.API.Controllers
{
    [Route("api/menu-item")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenuItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuItems()
        {
            var menuitems = await _mediator.Send(new GetMenuItemsQuery());
            return menuitems is null ? NotFound() : Ok(menuitems);
        }

        [HttpGet("{id:int}", Name = "GetMenuItem")]
        public async Task<IActionResult> GetMenuItem(int id)
        {
            var menuitem = await _mediator.Send(new GetMenuItemQuery(id));
            return menuitem is null ? NotFound() : Ok(menuitem);
        }

        [HttpGet("generate-error")]
        public IActionResult GenerateError()
        {
            int x = 1, y = 0;
            int z = x / y;
            return Ok(z);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateMenuItemCommand command)
        {
            var menuItemId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetMenuItem), new { id = menuItemId }, menuItemId);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] UpdateMenuItemCommand command)
        {
            if (id != command.MenuItem.Id) return BadRequest();

            var result = await _mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _mediator.Send(new DeleteMenuItemCommand(id));

            return result ? NoContent() : NotFound();
        }
    }
}