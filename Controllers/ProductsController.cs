using DatabaseConnection;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ProductsControler
{
	[Route("api/v1/products/")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		DbConn dbConn;
		public ProductsController()
		{
			dbConn = new DbConn();

		}
		[HttpPost("add/")]
		public ActionResult AddProducts([FromBody] Product product)
		{
			/*adding the product to the database*/
			try
			{
				dbConn.Products.Add(product);
				dbConn.SaveChanges();
				return Ok("Products added successfully");
			}catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			
		}

		[HttpGet("get/all")]
		public ActionResult<List<Product>> GetProducts()
		{
			var allproducts = dbConn.Products.ToList();
			if (allproducts != null)
			{
				return Ok(allproducts);
			}
			else
			{
				return Ok(null);
			}

		}

		[HttpGet("get/{id}")]
		public ActionResult<Product> GetProduct(int id)
		{
			var product = dbConn.Products.FirstOrDefault(e=>e.ProductId == id);
			if(product!=null)
			{
				return Ok(product);
			}else
			{
				return Ok(null);
			}

		}
		[HttpDelete("delete/{id}")]
		public ActionResult DeleteProduct(int id)
		{
			var product = dbConn.Products.FirstOrDefault(e => e.ProductId == id);
			if (product != null)
			{
				dbConn.Products.Remove(product);
				dbConn.SaveChanges();
				return Ok($"Product with id of {id} removed successfuly");
			}
			else
			{
				return Ok("Product with that iD does not exists");
			}

		}

		[HttpPatch("update/product/")]
		public ActionResult UpdateProduct([FromBody] Product product)
		{
			var Dbproduct = dbConn.Products.FirstOrDefault(e=>e.ProductId==product.ProductId);
			if(Dbproduct!=null)
			{
				dbConn.Products.Update(product);
				dbConn.SaveChanges();
				Response.Headers.Add("msg", $"product with {product.ProductId} updated successfully");
				return NoContent();
				/*return Ok($"Product with id of {product.ProductId} updated successfully");*/
				
			}
			else
			{
				return NotFound();
			}
		}

	}
}