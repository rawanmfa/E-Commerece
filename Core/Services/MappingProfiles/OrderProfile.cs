using AutoMapper;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
	public class OrderProfile : Profile
	{
		public OrderProfile() 
		{
			CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();
			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(o => o.ProductId, d => d.MapFrom(s => s.Product.ProductId))
				.ForMember(o => o.PictureUrl, d => d.MapFrom(s => s.Product.PictureUrl))
				.ForMember(o => o.ProductName, d => d.MapFrom(s => s.Product.ProductName));
			CreateMap<Order, OrderResultDto>()
				.ForMember(o => o.paymentStatus, d => d.MapFrom(s => s.ToString()))
				.ForMember(o => o.DeliveryMethod, d => d.MapFrom(s => s.DeliveryMethod.ShortName))
				.ForMember(o => o.Total, d => d.MapFrom(s => s.SubTotal+s.DeliveryMethod.Price));
			CreateMap<DeliveryMethod, DeliveryMethodResult>().ReverseMap();

		}
	}
}
