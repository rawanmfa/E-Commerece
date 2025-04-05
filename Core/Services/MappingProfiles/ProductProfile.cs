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
	public class ProductProfile:Profile
	{
		public ProductProfile() 
		{
			CreateMap<Product, ProductResultDTO>()
				.ForMember(d => d.BrandName, options => options.MapFrom(m => m.ProductBrand.Name))
				.ForMember(d => d.TypeName, options => options.MapFrom(m => m.ProductType.Name))
				.ForMember(d=>d.PictureUrl , options => options.MapFrom<PictureUrlResolver>());
			CreateMap<ProductBrand, BrandResultDTO>();
			CreateMap<ProductType, TypeResultDTO>();
		}
	}
}
