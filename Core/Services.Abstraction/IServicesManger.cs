﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface IServicesManger
	{
		public IProductService ProductService { get; }
		public IBasketService BasketService { get; }
		public IAuthenticationService AuthenticationService { get; }
		public IOrderService OrderService { get; }
		public ICashService CashService { get; }
	}
}
