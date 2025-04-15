using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using persistence;
using persistence.Data;
using persistence.Repositories;
using StackExchange.Redis;

namespace E_Commerece.Extensions
{
	public static class InfraStructureServiceExtension
	{
		public static IServiceCollection AddInfraStructureSevice(this IServiceCollection Services, IConfiguration configuration)
		{
			Services.AddScoped<IDbInitializer, DbInitializer>();
			Services.AddScoped<IUnitOfWork, UnitOfWork>();
			Services.AddScoped<IBasketRepository, BasketRepository>();
			Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});
			Services.AddSingleton<IConnectionMultiplexer>(
				_=>ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));
			return Services;
		}
	}
}
