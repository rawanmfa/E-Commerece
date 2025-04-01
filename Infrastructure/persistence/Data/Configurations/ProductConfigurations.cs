using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Data.Configurations
{
	public class ProductConfigurations : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(product => product.Price).HasColumnType("decimal(18,3)");
			builder.HasOne(product => product.ProductBrand).WithMany().HasForeignKey(product => product.ProductBrandId);
			builder.HasOne(product => product.ProductType).WithMany().HasForeignKey(product => product.ProductTypeId);
		}
	}
}
