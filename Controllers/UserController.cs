using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using userModel;

[Route("api/v1/users")]
[ApiController]
public class UserController : ControllerBase
{
	public List<Users> usersList;

	public UserController()
	{
		usersList = new List<Users>();
		for (int i = 0; i < 50; i++)
		{
			Users user = new Users()
			{
				user_email = $"{i}@gmail.com",
				user_password = $"{i}password",
				user_phone = $"{i}userphone"
			};
			usersList.Add(user);
		}
	}

	[HttpGet("all")]
	public ActionResult<List<Users>> GetAllUsers()
	{
		return Ok(usersList);
	}
}
