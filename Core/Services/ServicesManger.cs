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

		public ServicesManger(IUnitOfWork unitOfWork , IMapper mapper)
		{
			_productService = new Lazy<IProductService> (()=>new ProductService(unitOfWork , mapper));
		}

		public IProductService ProductService => _productService.Value;
	}
}
