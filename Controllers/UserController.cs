using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
	[Authorize]
	public ActionResult<List<Users>> GetAllUsers()
	{
		return Ok(usersList);
	}

	[HttpGet("get/user/{id}")]
	public ActionResult<Users> GetUserByID(int id)
	{
		Users user = usersList.FirstOrDefault(e => e.Id == id);

		if (user == null)
		{
			return NotFound();
		}

		return Ok(user);
	}

	[HttpDelete("user/delete/{id}")]
	public ActionResult DeleteUser(int id)
	{
		Users user = usersList.FirstOrDefault(e => e.Id == id);

		if (user == null)
		{
			return NotFound();
		}

		usersList.Remove(user);
		return Ok("User deleted successfully");
	}

	[HttpPost("users/add/new")]
	public ActionResult AddNewUser([FromBody] Users user)
	{
		if (user == null)
		{
			return BadRequest("Body required");
		}

		usersList.Add(user);
		return Ok("User added successfully");
	}

	[HttpPut("user/update/{id}")]
	public ActionResult UpdateUser(int id, [FromBody] Users updatedUser)
	{
		Users user = usersList.FirstOrDefault(e => e.Id == id);

		if (user == null)
		{
			return NotFound();
		}

		user.user_email = updatedUser.user_email;
		user.user_password = updatedUser.user_password;
		user.user_phone = updatedUser.user_phone;

		return Ok("User updated successfully");
	}

	[HttpPatch("user/modify/{id}")]
	public ActionResult ModifyUser(int id, [FromBody] Users newuser)
	{
		Users user = usersList.FirstOrDefault(e => e.Id == id);

		if (user == null)
		{
			return NotFound();
		}

		// Apply partial updates to the existing user
		if (newuser.user_email != null)
		{
			user.user_email = newuser.user_email;
		}

		if (newuser.user_password != null)
		{
			user.user_password = newuser.user_password;
		}

		if (newuser.user_phone != null)
		{
			user.user_phone = newuser.user_phone;
		}

		return Ok("User modified successfully");
	}
}
