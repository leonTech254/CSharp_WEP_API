using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseConnection;
using Microsoft.AspNetCore.Mvc;

namespace ProductsService_namespace
{
	public interface IProductService
	{
		ActionResult AddProduct(Product product);
		/*ActionResult<List<Product>> GetProducts();*/
		ActionResult<Product> GetProduct(int id);
		ActionResult DeleteProduct(int id);
		ActionResult UpdateProduct(Product product);
	}

	public class ProductService : IProductService
	{
		private readonly DbConn _dbConn;

		public ProductService(DbConn dbConn)
		{
			_dbConn = dbConn;
		}

		public ActionResult AddProduct(Product product)
		{
			try
			{
				_dbConn.Products.Add(product);
				_dbConn.SaveChanges();
				return new OkObjectResult("Product added successfully");
			}
			catch (Exception ex)
			{
				return new BadRequestObjectResult(ex.Message);
			}
		}

		public ActionResult<PaginatedResult<Product>> GetProducts(int page, int pageSize)
		{
			var totalItems = _dbConn.Products.Count();
			var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

			var paginatedData = _dbConn.Products
				.OrderBy(p => p.ProductId) 
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			var paginatedResult = new PaginatedResult<Product>
			{
				CurrentPage = page,
				TotalPages = totalPages,
				TotalItems = totalItems,
				Items = paginatedData
			};

			return new OkObjectResult(paginatedResult);
		}



		public ActionResult<Product> GetProduct(int id)
		{
			var product = _dbConn.Products.FirstOrDefault(e => e.ProductId == id);
			return product != null ? new OkObjectResult(product) : new OkObjectResult(null);
		}

		public ActionResult DeleteProduct(int id)
		{
			var product = _dbConn.Products.FirstOrDefault(e => e.ProductId == id);
			if (product != null)
			{
				_dbConn.Products.Remove(product);
				_dbConn.SaveChanges();
				return new OkObjectResult($"Product with ID {id} removed successfully");
			}
			else
			{
				return new OkObjectResult("Product with that ID does not exist");
			}
		}

		public ActionResult UpdateProduct(Product product)
		{
			var dbProduct = _dbConn.Products.FirstOrDefault(e => e.ProductId == product.ProductId);
			if (dbProduct != null)
			{
				_dbConn.Products.Update(product);
				_dbConn.SaveChanges();
				return new NoContentResult();
			}
			else
			{
				return new NotFoundResult();
			}
		}

		internal ActionResult<List<Product>> FilterProducts(string productName, decimal? price)
		{
			var query = _dbConn.Products.AsQueryable(); 

			if (!string.IsNullOrEmpty(productName))
			{
				query = query.Where(p => p.ProductName.Contains(productName));
			}

			if (price.HasValue)
			{
				/*query = query.Find(p => p.Price == price.Value);*/
				query = query.Where(p => p.Price == price.Value);
			}

			var filteredProducts = query.ToList();

			return new OkObjectResult(filteredProducts);
		}

	}
}
