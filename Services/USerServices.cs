using Azure;
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

		public ActionResult GetAllUsers()
		{
			var users = dbConn.Users.ToList();
			if(users == null)
			{
				return new OkObjectResult(null);
			}else
			{
				return new OkObjectResult(users);
			}
		}

		public ActionResult GetOneUser(int id)
		{
			var user=dbConn.Users.FirstOrDefault(e=>e.Id==id);
			if(user!=null)
			{
				return new OkObjectResult(user);
			}else
			{
				return new BadRequestResult();
			}
		}

		public ActionResult DeteUserById(int id)
		{
			var user = dbConn.Users.FirstOrDefault(e => e.Id == id);
			if(user!=null)
			{
				dbConn.Users.Remove(user); 
				dbConn.SaveChanges();
				return new OkObjectResult($"user with id {id} deleted successfully");
			}else
			{
				return new NotFoundResult();
			}
		}

		public ActionResult UpdateUSer(Users user,int id)
		{
			
			var userdb = dbConn.Users.FirstOrDefault(e => e.Id == id);
			if (userdb != null)
			{
				dbConn.Users.Update(user);
				return new OkObjectResult($"user with id {id} Update successfully");
			}
			else
			{
				return new NotFoundResult();
			}

		}
		public ActionResult? LoginUser(String username,String password)
		{
			var user = dbConn.Users.FirstOrDefault(e => e.user_name == username);
			if (user != null)
			{
				//getting user password from the db
				String hashedpassword = user.user_password;
				bool iscorrect=passwordEncryption.PasswordVerifation(password, hashedpassword);
				if(iscorrect)
				{
					return new OkObjectResult("Login Successfully");
				}else
				{
					return new BadRequestResult();
				}

			}else
			{
				return new NotFoundResult();

			}
		


		}

		



	}

}