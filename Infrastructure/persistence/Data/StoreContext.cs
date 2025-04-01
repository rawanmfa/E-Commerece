﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Data
{
	public class StoreContext:DbContext
	{
		public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> productBrands { get; set; }
		public DbSet<ProductType> productTypes { get; set; }
	}
}
