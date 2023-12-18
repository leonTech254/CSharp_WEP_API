using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int OrderId { get; set; }

	[Required]
	public DateTime OrderDate { get; set; }

	public String OrderStatus { get; set; }

	[ForeignKey("Products")]
	public int ProductID { get; set; }

	[ForeignKey("Users")]
	public int userId {  get; set; }

	public virtual Product Product { get; set; }


}