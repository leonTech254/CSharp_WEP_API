using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService_namespace;

namespace OrderController
{
	[Route("api/v1/orders/")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly OrderService _orderService;

		public OrderController(OrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost("add/")]
		[Authorize]
		public ActionResult<Order> AddOrder([FromBody] Order order)
		{
			// Simulate extracting token from Authorization header
			string jwtToken = HttpContext.Request.Headers["Authorization"];
			if (jwtToken != null)
			{
				String token = jwtToken.ToString().Replace("Bearer ", "");

				// Call the service to add the order
				var addedOrder = _orderService.placeOrder(order,token);

				return Ok(addedOrder);
			}
			else
			{
				return NotFound();
			}
		}

		[HttpGet("get/all")]
		public ActionResult<IQueryable<Order>> GetOrders()
		{
			// Call the service to get all orders
			var orders = _orderService.GetOrders();
			return Ok(orders);
		}

		[HttpGet("get/{id}")]
		public ActionResult<Order> GetOrder(int id)
		{
			// Call the service to get an order by id
			var order = _orderService.GetOrder(id);
			if (order == null)
			{
				Response.Headers.Add("msg", $"The order with id {id} does not exist");
				return NotFound();
			}
			else
			{
				return Ok(order);
			}
		}

		[HttpDelete("delete/{id}")]
		public ActionResult DeleteOrder(int id)
		{
			// Call the service to delete an order by id
			var result = _orderService.DeleteOrder(id);
			if (result)
			{
				return Ok("Order removed successfully");
			}
			else
			{
				Response.Headers.Add("msg", $"The order with id {id} does not exist");
				return NotFound();
			}
		}

		[HttpPatch("update/{id}")]
		public ActionResult UpdateOrderStatus(int id, [FromBody] string newStatus)
		{
			// Call the service to update the order status
			var result = _orderService.UpdateOrderStatus(id, newStatus);
			if (result)
			{
				return Ok("Order status updated successfully");
			}
			else
			{
				Response.Headers.Add("msg", $"The order with id {id} does not exist");
				return NotFound();
			}
		}
	}
}
