using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using userModel;
using USerService;

namespace UserControlller_NameSpace
{
	[Route("api/v1/user/")]
	[ApiController]
	public class UserControllerCl
	{
		UserService uSerService;
		public UserControllerCl() {
			uSerService = new UserService();
		
		}




		[HttpPost("add/")]
		public ActionResult AddNewUser([FromBody] Users user)
		{
			return uSerService.AddUser(user);
		}

		[HttpPost("login/")]
		public ActionResult UserLogin([FromBody] Users user )
		{
			String username = user.user_name;
			String password = user.user_password;
			return uSerService.LoginUser(username, password);
		}

		[HttpGet("get/")]
		public ActionResult GetAllUSers()
		{
			return uSerService.GetAllUsers();
		}

		[HttpDelete("delete/{id}")]
		public ActionResult DeleteUser(int id)
		{
			return uSerService.DeteUserById(id);
		}

		[HttpPatch("update/{id}")]
		public ActionResult UpdateUSer([FromBody] Users user, int id)
		{
			return uSerService.UpdateUSer(user,id);

		}
	}
}