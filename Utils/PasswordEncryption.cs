
using System.Security.Cryptography;
using System.Text;

namespace PasswordEncryption_namespase

{
	public class PasswordEncryption
	{
		public PasswordEncryption()
		{

		}
		public String EncryptPassword(String rawPassword)
		{
			using (SHA256 sHA256 = SHA256.Create()) {

				byte[] hashedBytes=sHA256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
				String encrPasword=BitConverter.ToString(hashedBytes).Replace("-","");
				return encrPasword;
			}
		}
	}
}