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
				Id = i,
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

	[HttpGet("get/user/{id}")]
	public ActionResult<Users> GetUserByID(int id)
	{
		List<Users> users = usersList;
		Users user=users.FirstOrDefault(e => e.Id == id);

		if(user == null)
		{
			return NotFound();
		}

		return Ok(user);
	}

	[HttpDelete("user/delete/{id}")]
	public ActionResult DeleteUser(int id)
	{
		List<Users> users=usersList;
		Users user = users.FirstOrDefault(e => e.Id == id);
		users.Remove(user);
		return Ok("User deete successfully");
	}

	[HttpPost("users/add/new")]
	public ActionResult AddNewUser([FromBody] Users user)
	{
		if (user == null)
		{
			return BadRequest("Body required");

		}
		usersList.Add(user);
		return Ok("User Added succesfully");

	}

}
