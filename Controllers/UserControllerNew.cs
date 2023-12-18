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
			uSerService.AddUser(user);
		}
	}
}