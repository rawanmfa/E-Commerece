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
		public Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string email);
		public Task<OrderResultDto> CreateOrderAsync(OrderRequest request,string userEmail);
		public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
	}
}
