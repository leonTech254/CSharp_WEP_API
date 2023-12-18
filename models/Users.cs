using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace userModel
{
	public class Users
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[StringLength(100)]
		public String user_name { get; set; }
		[StringLength(100)]
		public string user_email { get; set; } 
		[StringLength(100)]
		public string user_password { get; set; }
		[StringLength(100)]
		public string user_phone { get; set; }
		
		public String role { get; set; }


		//defaults
		public Users()
		{
			if (string.IsNullOrEmpty(role))
			{
				role = "USER";
			}
		}
	}
}
	