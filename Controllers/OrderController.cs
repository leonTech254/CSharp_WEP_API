using DatabaseConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace OrderController
{


	[Route("api/v1/orders/")]
	public class ProductController:ControllerBase
	{
		DbConn dbConn;
		public ProductController() { 
			dbConn = new DbConn();
	
		}

		[HttpPost("add/")]
		public ActionResult AddOrder([FromBody] Order order)
		{
			dbConn.orders.Add(order);
			dbConn.SaveChanges();
			return Ok("Order placed successfully");

		}

		[HttpGet("get/all")]
		public ActionResult<List<Order>> GetOrders()
		{
			var orders = dbConn.orders;
			return Ok(orders);
		}

		[HttpGet("get/{id}")]
		public ActionResult<Order> Getorder(int id)
		{
			var order = dbConn.orders.FirstOrDefault(e=>e.OrderId == id);
			if(order == null)
			{
				return Ok(order);
			}else
			{
				Response.Headers.Add("msg",$"the product with id {id} does not exist");
				return NotFound();
			}

		}
		[HttpDelete("delete/{id}")]
		public ActionResult<Order> DeleteOrder(int id)
		{
			var order = dbConn.orders.FirstOrDefault(e => e.OrderId == id);
			if (order == null)
			{
				dbConn.orders.Remove(order); 
				dbConn.SaveChanges();
				return Ok("Product removed successfully");
			}
			else
			{
				Response.Headers.Add("msg", $"the product with id {id} does not exist");
				return NotFound();
			}

		}
		[HttpPatch("update/{id}")]
		public ActionResult<Order> UpdateProduct(int id)
		{
			var order = dbConn.orders.FirstOrDefault(e => e.OrderId == id);
			if (order == null)
			{
				dbConn.orders.Update(order);
				dbConn.SaveChanges();
				return Ok("Product Upfate successfully");
			}
			else
			{
				Response.Headers.Add("msg", $"the product with id {id} does not exist");
				return NotFound();
			}

		}


	}

}