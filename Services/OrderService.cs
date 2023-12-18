using DatabaseConnection;
using JwTNameService;
using Microsoft.AspNetCore.Mvc;

namespace OrderService_namespace
{

	public class OrderService
	{
		private DbConn dbConn;
		private Jwt jwt;
		public OrderService() { 
			dbConn = new DbConn();
		}

		public ActionResult placeOrder(Order order,String token)
		{
			if(order != null) {
				//getting userId from the token
				String StringId=jwt.GetUserIdfromTheToken(token);
				int userId=int.Parse(StringId);

				///adding the user adding to the Order
			order.OrderId = userId;
			dbConn.orders.Add(order);
				dbConn.SaveChanges();
				return new OkObjectResult("Order placed Successfully");
			}
			else
			{
				return new OkObjectResult("order cannot be empty,Please provide the required data");

			}

		}
	}

}