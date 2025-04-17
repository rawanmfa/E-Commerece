using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace presentation
{
	[Authorize]
	public class OrderController(IServicesManger servicesManger) : ApiController
	{
		[HttpPost]
		public async Task<ActionResult<OrderResultDto>> Create(OrderRequest request)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var order = await servicesManger.OrderService.CreateOrderAsync(request, email);
			return Ok(order);
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetOrders()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var orders = await servicesManger.OrderService.GetOrdersByEmailAsync(email);
			return Ok(orders);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderResultDto>> GetOrderById(Guid id)
		{
			var order = await servicesManger.OrderService.GetOrderByIdAsync(id);
			return Ok(order);
		}
		[AllowAnonymous]
		[HttpGet("DeliveryMethod")]
		public async Task<ActionResult<DeliveryMethodResult>> GetDeliveryMethods()
		{
			var methods = await servicesManger.OrderService.GetDeliveryMethodsAsync();
			return Ok(methods);
		}
	}
}
