using Services;
using Services.Abstractions;

namespace E_Commerece.Extensions
{
	public static class CoreServiceExtensions
	{
		public static IServiceCollection AddCoreServices (this IServiceCollection services)
		{
			services.AddScoped<IServicesManger, ServicesManger>();
			services.AddAutoMapper(typeof(Services.AssemplyReference).Assembly);
			return services;
		}
	}
}
