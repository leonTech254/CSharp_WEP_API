using Microsoft.EntityFrameworkCore;
using userModel;

namespace DatabaseConnection

{

	public class DbConn : DbContext

	{

		public DbSet<Users> Users { get; set; }
		public DbSet<Order> orders { get; set; }
		public DbSet<Product> Products { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

		{

			optionsBuilder.UseSqlServer(@"Server=DESKTOP-2KRMLJS\MSSQLSERVER01;Database=Usermanagement;Trusted_Connection=True;TrustServerCertificate=True;");

		}

	}

}