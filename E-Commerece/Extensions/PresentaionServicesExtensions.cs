using E_Commerece.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.Extensions
{
	public static class PresentaionServicesExtensions
	{
		public static IServiceCollection AddPresentaionServices(this IServiceCollection Services)
		{
			Services.AddControllers().AddApplicationPart(typeof(presentation.AssemplyReference).Assembly);
			Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = ApiResponseFactory.customValidationErrorResponse;
			});
			Services.AddEndpointsApiExplorer();
			Services.AddSwaggerGen();
			return Services;
		}
	}
}
