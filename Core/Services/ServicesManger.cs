using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAuthenticationService = Services.Abstractions.IAuthenticationService;

namespace Services
{
	public class ServicesManger : IServicesManger
	{
		private readonly Lazy<IProductService> _productService;
		private readonly Lazy<IBasketService> _basketService;
		private readonly Lazy<IAuthenticationService> _authenticationService;


		public ServicesManger(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<User> userManager, IConfiguration configuration, IOptions<JwtOptions> options)
		{
			_productService = new Lazy<IProductService> (()=>new ProductService(unitOfWork , mapper));
			_basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
			_authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, configuration, options));
		}

		public IProductService ProductService => _productService.Value;

		public IBasketService BasketService => _basketService.Value;

		public IAuthenticationService AuthenticationService => _authenticationService.Value;
	}
}
