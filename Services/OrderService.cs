using DatabaseConnection;
using JwTNameService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace OrderService_namespace
{
	public class OrderService
	{
		private DbConn dbConn;
		private readonly Jwt _jwt;

		public OrderService(Jwt jwt)
		{
			dbConn = new DbConn();
			_jwt = jwt;
		}

		public ActionResult placeOrder(Order order, string token)
		{
			if (order != null)
			{
				// Getting userId from the token
				string StringId = _jwt.GetUserIdFromToken(token);
				int userId = int.Parse(StringId);

				// Adding the user ID to the Order
				order.userId = userId;
				dbConn.orders.Add(order);
				dbConn.SaveChanges();
				return new OkObjectResult("Order placed successfully");
			}
			else
			{
				return new OkObjectResult("Order cannot be empty, please provide the required data");
			}
		}

		public bool UpdateOrderStatus(int id, string newStatus)
		{
			var order = dbConn.orders.FirstOrDefault(e => e.OrderId == id);
			if (order != null)
			{
				// Update the order status here using newStatus
				order.OrderStatus = newStatus;
				dbConn.orders.Update(order);
				dbConn.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool DeleteOrder(int id)
		{
			var order = dbConn.orders.FirstOrDefault(e => e.OrderId == id);
			if (order != null)
			{
				dbConn.orders.Remove(order);
				dbConn.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public Order GetOrder(int id)
		{
			var order = dbConn.orders.FirstOrDefault(e => e.OrderId == id);
			return order;
		}

		public IQueryable<Order> GetOrders()
		{
			var orders = dbConn.orders;
			return orders;
		}
	}
}
