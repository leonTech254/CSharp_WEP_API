using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using userModel;

namespace JwTNameService
{
	public class Jwt
	{
		private readonly IConfiguration _configuration;
		public Jwt(IConfiguration configuration) {
			_configuration = configuration;
		
		}

		public Jwt()
		{

		}

		public String? JwTGenerateToken(Users user)
		{
			/*String issuer = _configuration.GetSection("JwtOptions:issuer").Value;
			String secret_key = _configuration.GetSection("JwtOptions:secrete_Key").Value;
			String Audience = _configuration.GetSection("JwtOptions:Audience").Value;*/

			String issuer = "martin_leon";
			String secret_key = "leonMartin234567deggllsdjvbhvbshfvbshdvsjdvhbsjvsvQOQCMASCNQFECJEJDRJ";
			String Audience = "users";

			var SecurityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret_key));
			var Credentials= new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);


			//claims
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name,user.user_name),
				new Claim(ClaimTypes.Role,user.role),
				new Claim("user_id" ,$"{user.Id}"),
			};
		

			var token = new JwtSecurityToken(
				issuer, //issuer
				Audience, //audience
				claims, //claims
				DateTime.Now,
				DateTime.Now.AddMinutes(30), //expire date
				Credentials //credentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}


		public string? GetUSernamefromTheToken(string jwtToken)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

			var usernameClaim = token?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);

			return usernameClaim?.Value;
		}
	}
}