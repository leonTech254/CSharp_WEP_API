using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsService_namespace;

namespace ProductsControler
{
	[Route("api/v1/products/")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ProductService _productService;
		public ProductsController(ProductService productService)
		{
			_productService= productService;
		}

		[HttpPost("add/")]
		[Authorize(Roles = "ADMIN")]
		public ActionResult AddProduct([FromBody] Product product)
		{
			return _productService.AddProduct(product);
		}

		[HttpGet("get/all")]
		public ActionResult<PaginatedResult<Product>> GetProducts()
		{
			return _productService.GetProducts(1,2);
		}

		[HttpGet("get/{id}")]
		public ActionResult<Product> GetProduct(int id)
		{
			return _productService.GetProduct(id);
		}

		[HttpDelete("delete/{id}")]
		[Authorize(Roles = "ADMIN")]
		public ActionResult DeleteProduct(int id)
		{
			return _productService.DeleteProduct(id);
		}

		[HttpPatch("update/product/")]
		[Authorize(Roles = "ADMIN")]
		public ActionResult UpdateProduct([FromBody] Product product)
		{
			return _productService.UpdateProduct(product);
		}

		[HttpGet("filter/")]
		public ActionResult<List<Product>> filterProducts([FromQuery] string productName, [FromQuery] decimal? price)
		{

			return _productService.FilterProducts(productName,price);
		}
	}
}
