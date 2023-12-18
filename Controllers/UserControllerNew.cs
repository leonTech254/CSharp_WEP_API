using LoginDTO_namespace;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using userModel;
using USerService_namespace;

namespace UserControlller_NameSpace
{
	[Route("api/v1/user/")]
	[ApiController]
	public class UserControllerCl
	{
		private readonly UserService _uSerService;
		public UserControllerCl(UserService userService) {
		_uSerService=userService;
		
		}




		[HttpPost("add/")]
		public ActionResult AddNewUser([FromBody] Users user)
		{
			return _uSerService.AddUser(user);
		}

		[HttpPost("login/")]
		public ActionResult UserLogin([FromBody] LoginDTO user )
		{
			String username = user.UserName;
			String password = user.password;
			return _uSerService.LoginUser(username, password);
		}

		[HttpGet("get/")]
		public ActionResult GetAllUSers()
		{
			return _uSerService.GetAllUsers();
		}

		[HttpDelete("delete/{id}")]
		public ActionResult DeleteUser(int id)
		{
			return _uSerService.DeteUserById(id);
		}

		[HttpPatch("update/{id}")]
		public ActionResult UpdateUSer([FromBody] Users user, int id)
		{
			return _uSerService.UpdateUSer(user,id);

		}
	}
}