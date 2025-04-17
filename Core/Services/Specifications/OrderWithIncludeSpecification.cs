using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
	public class OrderWithIncludeSpecification : Specifications<Order>
	{
		public OrderWithIncludeSpecification(Guid id):base(o=>o.Id==id)
		{
			AddInclude(o => o.DeliveryMethod);
			AddInclude(o=>o.OrderItems);
		}
		public OrderWithIncludeSpecification(string email) : base(o => o.UserEmail==email)
		{
			AddInclude(o => o.DeliveryMethod);
			AddInclude(o => o.OrderItems);
			SetOrderBy(o=>o.OrderDate);
		}

	}
}
