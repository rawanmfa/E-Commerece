﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Data.Configurations
{
	public class OrderConfigurations : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.OwnsOne(d => d.ShippingAddress, a => a.WithOwner());
			builder.HasMany(o => o.OrderItems).WithOne();
			builder.Property(o => o.paymentStatus).HasConversion(s => s.ToString(), s => Enum.Parse<OrderPaymentStatus>(s));
			builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
			builder.Property(o => o.SubTotal).HasColumnType("decimal(18,3)");
		}
	}
}
