using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class DeliveryMethod:BaseEntity<int>
	{
		public DeliveryMethod() { }
		public DeliveryMethod(string _shortName, string _description, string _deliveryTime, decimal _price)
		{
			ShortName = _shortName;
			Description = _description;
			DeliveryTime = _deliveryTime;
			Price = _price;
		}

		public string ShortName { get; set; }
		public string Description { get; set; }
		public string DeliveryTime { get; set; }
		public decimal Price { get; set; }
	}
}
