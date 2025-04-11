using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public sealed class BasketNotFoundException(string id) : NotFoundException($"Basket With Id {id} not found")
	{
	}
}
