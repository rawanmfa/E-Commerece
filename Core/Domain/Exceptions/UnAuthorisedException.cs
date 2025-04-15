using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public class UnAuthorisedException(string message="invalid email or password failed"): Exception(message)
	{
	}
}
