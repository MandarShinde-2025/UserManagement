using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.CQRS.Commands.Orders;
using UserManagement.Application.CQRS.Queries.Orders;

namespace UserManagement.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            return orders is null ? NotFound() : Ok(orders);
        }

        [HttpGet("{id:int}", Name = "GetOrder")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _mediator.Send(new GetOrderQuery(id));
            return order is null ? NotFound() : Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrder), new { id = orderId }, orderId);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderCommand command)
        {
            if (id != command.Order.Id) return BadRequest();

            var result = await _mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id));
            return result ? NoContent() : NotFound();
        }
    }
}