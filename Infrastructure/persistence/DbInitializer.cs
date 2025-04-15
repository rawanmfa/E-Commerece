using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace persistence
{
	public class DbInitializer : IDbInitializer
	{
		private readonly StoreContext _storeContext;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<User> _userManager;

		public DbInitializer(StoreContext storeContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			_storeContext = storeContext;
			_roleManager = roleManager;
			_userManager = userManager;
		}

		public async Task InitializeAsync()
		{
			try
			{
				if (_storeContext.Database.GetPendingMigrations().Any())
				{
					await _storeContext.Database.MigrateAsync();
				}
				if (!_storeContext.productTypes.Any())
				{
					var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\persistence\Data\Seeding\types.json");
					var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
					if (types is not null && types.Any())
					{
						await _storeContext.productTypes.AddRangeAsync(types);
						await _storeContext.SaveChangesAsync();
					}
				}
				if (!_storeContext.productBrands.Any())
				{
					var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\persistence\Data\Seeding\brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
					if (brands is not null && brands.Any())
					{
						await _storeContext.productBrands.AddRangeAsync(brands);
						await _storeContext.SaveChangesAsync();
					}
				}
				if (!_storeContext.Products.Any())
				{
					var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\persistence\Data\Seeding\products.json");
					var products = JsonSerializer.Deserialize<List<Product>>(productsData);
					if (products is not null && products.Any())
					{
						await _storeContext.Products.AddRangeAsync(products);
						await _storeContext.SaveChangesAsync();
					}
				}
			}
			catch (Exception) 
			{ throw; }
		}

		public async Task InitializeIdentityAsync()
		{
            if (!_roleManager.Roles.Any())
            {
				await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
				await _roleManager.CreateAsync(new IdentityRole("Admin"));
			}
            if (! _userManager.Users.Any())
            {
				var SuperAdminUser = new User
				{
					DisplayName = "SuperAdmin",
					Email = "SuperAdminUser@gmail.com",
					UserName = "SuperAdminUser",
					PhoneNumber = "0113456789",
				};
				var AdminUser = new User
				{
					DisplayName = "Admin",
					Email = "AdminUser@gmail.com",
					UserName = "AdminUser",
					PhoneNumber = "0123456789",
				};
				await _userManager.CreateAsync(SuperAdminUser,"Passw0rd"); // password
				await _userManager.CreateAsync(AdminUser, "Passw0rd");
				await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
				await _userManager.AddToRoleAsync(AdminUser, "Admin");
			}
		}
	}
}
