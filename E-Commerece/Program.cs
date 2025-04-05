
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using persistence;
using persistence.Data;
using persistence.Repositories;
using Services;
using Services.Abstractions;

namespace E_Commerece
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers().AddApplicationPart(typeof(presentation.AssemplyReference).Assembly);
			builder.Services.AddScoped<IDbInitializer,DbInitializer>();
			builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
			builder.Services.AddScoped<IServicesManger, ServicesManger>();
			builder.Services.AddAutoMapper(typeof(Services.AssemplyReference).Assembly);
			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			await InitializeDbAsync(app);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStaticFiles();

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();

			async Task InitializeDbAsync(WebApplication app)
			{
				using var scope = app.Services.CreateScope();
				var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
				await dbInitializer.InitializeAsync();
			}
		}
	}
}
