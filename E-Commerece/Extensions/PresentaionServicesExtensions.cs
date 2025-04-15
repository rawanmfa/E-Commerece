using E_Commerece.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

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
			Services.ConfigreSwagger();
			return Services;
		}
		public static IServiceCollection ConfigreSwagger(this IServiceCollection Services)
		{
			Services.AddEndpointsApiExplorer();
			Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", securityScheme: new Microsoft.OpenApi.Models.OpenApiSecurityScheme
				{
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Description = "please enter Bearer Token",
					Name = "Autherization",
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT"
				});
				options.AddSecurityRequirement(securityRequirement: new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
				{
					{
					new OpenApiSecurityScheme
					{
						Reference=new OpenApiReference
						{
							Type= ReferenceType.SecurityScheme,
							Id="Bearer"
						}
					},
					new List<string>(){} }
				});
				
			});
			return Services;
		}
	}
}
