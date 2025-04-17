using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Order:BaseEntity<Guid>
	{
		public Order() { }
		public Order(string _userEmail, ShippingAddress _shippingAddress, ICollection<OrderItem> _orderItems, DeliveryMethod _deliveryMethod, decimal _subTotal)
		{
			UserEmail = _userEmail;
			ShippingAddress = _shippingAddress;
			OrderItems = _orderItems;
			DeliveryMethod = _deliveryMethod;
			SubTotal = _subTotal;
		}
		public string UserEmail { get; set; }
		public ShippingAddress ShippingAddress { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; }
		public OrderPaymentStatus paymentStatus {  get; set; }
		public int? DeliveryMethodId { get; set; }
		public DeliveryMethod DeliveryMethod { get; set; }
		public decimal SubTotal { get; set; }
		public string PaymentIntendId { get; set; } = string.Empty;
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
	}
}
