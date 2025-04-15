﻿using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Identity
{
	public class StoreIdentityContext:IdentityDbContext
	{
		public StoreIdentityContext(DbContextOptions<StoreIdentityContext> options) : base(options) 
		{

		}
		#region on 
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Address>().ToTable("Addresses");
		}
		#endregion
	}
}
