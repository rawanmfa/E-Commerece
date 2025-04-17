using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public class DeliveryException(int id):NotFoundException($"no delivery method with id {id} was found")
	{

	}
}
