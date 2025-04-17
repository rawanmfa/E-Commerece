using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface IOrderService
	{
		public Task<OrderResultDto> GetOrderByIdAsync(Guid Id);
		public Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string Email);
		public Task<OrderResultDto> CreateOrderAsync(OrderResultDto Request,string UserEmail);
		public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
	}
}
