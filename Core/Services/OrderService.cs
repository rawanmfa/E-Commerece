//using AutoMapper;
//using Domain.Contracts;
//using Domain.Entities;
//using Domain.Exceptions;
//using Services.Abstractions;
//using Services.Specifications;
//using Shared;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services
//{
//	public class OrderService(IMapper mapper , IUnitOfWork unitOfWork, IBasketRepository basketRepository) : IOrderService
//	{
//		public async Task<OrderResultDto> CreateOrderAsync(OrderResultDto Request, string UserEmail)
//		{
//			var Address = mapper.Map<ShippingAddress>(Request.ShippingAddress);
//			var Basket = await basketRepository.GetBasketAsync(Request.BasketId)
//				??throw new BasketNotFoundException(Request.BasketId);
//			var orderItems = new List<OrderItem>();
//            foreach (var item in Basket.Items)
//            {
//				var product = await unitOfWork.GetRepository<Product, int>().GetAsync
//					(item.Id) ?? throw new BasketNotFoundException(Request.BasketId);
//                orderItems.Add(CreateOrderItem(item,product));
//			}
//			var deliveymethod = await unitOfWork.GetRepository<DeliveryMethod,int>()
//				.GetByIdWithSpecificationAsync(Request.DeliveryMethodId
//				?? throw new DeliveryException(Request.DeliveryMethodId);
//			var subtotal = orderItems.Sum(item=> item.Price*item.Quantity);
//			var order = new Order(UserEmail, subtotal, orderItems,Address,deliveymethod);
//			await unitOfWork.SaveChangesAsync();
//			return 
//        }

//		private object CreateOrderItem(BasketItem item, object product)
//		=> new OrderItem(new ProductInOrderItem(product.));

//		public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
//		{
//			var methods = await unitOfWork.GetRepository<DeliveryMethod, int>()
//				.GetAllAsync();
//				return mapper.Map<IEnumerable<DeliveryMethodResult>>(methods);
//		}

//		public async Task<OrderResultDto> GetOrderByIdAsync(Guid Id)
//		{
//			var order = await unitOfWork.GetRepository<Order,Guid>()
//				.GetByIdWithSpecificationAsync(new OrderWithIncludeSpecification(Id))
//				?? throw new OrderNotFoundException(Id);
//			return mapper.Map<OrderResultDto>(order);
//		}

//		public async Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string Email)
//		{
//			var orders = await unitOfWork.GetRepository<Order, Guid>()
//				.GetAllWithSpecificationAsync(new OrderWithIncludeSpecification(Email));
//			return mapper.Map<IEnumerable<OrderResultDto>>(orders);
//		}
//	}
//}
