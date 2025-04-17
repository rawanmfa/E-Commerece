using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using persistence;
using persistence.Data;
using persistence.Identity;
using persistence.Repositories;
using Shared;
using StackExchange.Redis;
using System.Text;

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
				_ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));
			Services.AddDbContext<StoreIdentityContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
			});
			Services.ConfigureIdentityService();
			Services.ConfigureJwt(configuration);
			return Services;
		}
		public static IServiceCollection ConfigureIdentityService(this IServiceCollection Services)
		{
			Services.AddIdentity<User, IdentityRole>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 5;
			}).AddEntityFrameworkStores<StoreIdentityContext>();

			return Services;
		}
		public static IServiceCollection ConfigureJwt(this IServiceCollection Services, IConfiguration configuration)
		{
			var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
			Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtOptions.issuer,
					ValidAudience = jwtOptions.audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
				};
			});
			Services.AddAuthorization();
			return Services;
		}

	}
}