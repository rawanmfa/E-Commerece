using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class ServicesManger : IServicesManger
	{
		private readonly Lazy<IProductService> _productService;
		private readonly Lazy<IBasketService> _basketService;

		public ServicesManger(IUnitOfWork unitOfWork , IMapper mapper,IBasketRepository basketRepository)
		{
			_productService = new Lazy<IProductService> (()=>new ProductService(unitOfWork , mapper));
			_basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
		}

		public IProductService ProductService => _productService.Value;

		public IBasketService BasketService => _basketService.Value;
	}
}
