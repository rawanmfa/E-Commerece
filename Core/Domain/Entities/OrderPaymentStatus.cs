using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public enum OrderPaymentStatus
	{
		pending = 0,
		paymentRecived = 1,
		paymentFailed = 2,
	}
}
