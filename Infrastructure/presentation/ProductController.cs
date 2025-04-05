using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController(IServicesManger ServicesManger) : ControllerBase
	{
		#region Get All Product
		[HttpGet("Products")]
		public async Task<ActionResult<IEnumerable<ProductResultDTO>>> GetAllProducts()
		{
			var products = await ServicesManger.ProductService.GetAllProductsAsync();
			return Ok(products);
		}
		#endregion
		#region Get All Brands
		[HttpGet("Brands")]
		public async Task<ActionResult<IEnumerable<BrandResultDTO>>> GetAllBrands()
		{
			var Brands = await ServicesManger.ProductService.GetAllBrandsAsync();
			return Ok(Brands);
		}
		#endregion
		#region Get All Types
		[HttpGet("Types")]
		public async Task<ActionResult<IEnumerable<TypeResultDTO>>> GetAllTypes()
		{
			var Types = await ServicesManger.ProductService.GetAllTypesAsync();
			return Ok(Types);
		}
		#endregion
		#region Get Product By Id
		[HttpGet("{id}")]
		public async Task<ActionResult<IEnumerable<ProductResultDTO>>> GetProductById(int id)
		{
			var product = await ServicesManger.ProductService.GetProductByIdAsync(id);
			return Ok(product);
		}
		#endregion

	}
}
