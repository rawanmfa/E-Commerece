using Domain.Contracts;
using E_Commerece.MiddleWare;

namespace E_Commerece.Extensions
{
	public static class WebApplicationExtensions
	{
		public static async Task<WebApplication> SeedDataBaseAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
			await dbInitializer.InitializeAsync();
			await dbInitializer.InitializeIdentityAsync();
			return app;
		}
		//public static WebApplication UseCustomExceptionsMiddleWare(this WebApplication app)
		//{
		//	app.UseMiddleware<GlobalErrorHandlingMiddleWare>();
		//}
	}
}
