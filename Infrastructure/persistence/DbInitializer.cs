using Domain.Contracts;
using Domain.Entities;
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

		public DbInitializer(StoreContext storeContext)
		{
			_storeContext = storeContext;
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
	}
}
