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
	public class BasketController(IServicesManger ServicesManger) : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<ActionResult<BasketDto>> Get(string id)
		{
			var basket = await ServicesManger.BasketService.GetBasketAsync(id);
			return Ok(basket);
		}
		[HttpPost]
		public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
		{
			var basket = await ServicesManger.BasketService.UpdateBasketAsync(basketDto);
			return Ok(basket);
		}
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(string id)
		{
			await ServicesManger.BasketService.DeleteBasketAsync(id);
			return NoContent();
		}

	}
}
