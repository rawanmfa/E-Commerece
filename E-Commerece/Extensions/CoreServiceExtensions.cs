using Services;
using Services.Abstractions;
using Shared;

namespace E_Commerece.Extensions
{
	public static class CoreServiceExtensions
	{
		public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IServicesManger, ServicesManger>();
			services.AddAutoMapper(typeof(Services.AssemplyReference).Assembly);
			services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
			return services;
		}
	}
}