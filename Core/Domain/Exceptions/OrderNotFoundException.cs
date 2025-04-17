using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public class OrderNotFoundException(Guid Id):NotFoundException($"no order with id {Id} was found")
	{
	}
}
