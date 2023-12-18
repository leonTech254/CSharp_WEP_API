using DatabaseConnection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PasswordEncryption_namespase;
using userModel;

namespace USerService
{
	public class UserService
	{
		DbConn dbConn;
		PasswordEncryption passwordEncryption;
		public UserService() { 

			dbConn = new DbConn();
			passwordEncryption = new PasswordEncryption();

		}


		public ActionResult AddUser(Users user)
		{
			if(user != null)
			{
				//getting the user password
				String userPass = user.user_password;
				String encryptedPassword=passwordEncryption.EncryptPassword(userPass);
				///updating uthe user password
				user.user_password=encryptedPassword;
				dbConn.Users.Add(user);
				dbConn.SaveChanges();
				return new OkObjectResult("user added succesfully");
			}else
			{
				return new BadRequestResult();

			}
			
		}



	}

}