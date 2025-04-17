using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class OrderService(IMapper mapper, IUnitOfWork unitOfWork, IBasketRepository basketRepository) : IOrderService
	{
		public async Task<OrderResultDto> CreateOrderAsync(OrderRequest request, string userEmail)
		{
			var Address = mapper.Map<ShippingAddress>(request.ShippingAddress);
			var Basket = await basketRepository.GetBasketAsync(request.BasketId)
				?? throw new BasketNotFoundException(request.BasketId);
			var orderItems = new List<OrderItem>();
			foreach (var item in Basket.Items)
			{
				var product = await unitOfWork.GetRepository<Product, int>().GetAsync
					(item.Id) ?? throw new ProductNotFoundException(item.Id);
				orderItems.Add(CreateOrderItem(item, product));
			}
			var deliveymethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
				.GetAsync(request.DeliveryMethodId)
				?? throw new DeliveryException(request.DeliveryMethodId);
			var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
			var order = new Order(userEmail,Address, orderItems, deliveymethod,subtotal);
			await unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
			await unitOfWork.SaveChangesAsync();
			return mapper.Map<OrderResultDto>(order);

		}

		private OrderItem CreateOrderItem(BasketItem item, Product product)
		=> new OrderItem(new ProductInOrderItem(product.Id,product.Name,product.PictureUrl),item.Quantity,product.Price);

		public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
		{
			var methods = await unitOfWork.GetRepository<DeliveryMethod, int>()
				.GetAllAsync();
			return mapper.Map<IEnumerable<DeliveryMethodResult>>(methods);
		}

		public async Task<OrderResultDto> GetOrderByIdAsync(Guid Id)
		{
			var order = await unitOfWork.GetRepository<Order, Guid>()
				.GetByIdWithSpecificationAsync(new OrderWithIncludeSpecification(Id))
				?? throw new OrderNotFoundException(Id);
			return mapper.Map<OrderResultDto>(order);
		}

		public async Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string email)
		{
			var orders = await unitOfWork.GetRepository<Order, Guid>()
				.GetAllWithSpecificationAsync(new OrderWithIncludeSpecification(email));
			return mapper.Map<IEnumerable<OrderResultDto>>(orders);
		}
	}
}
