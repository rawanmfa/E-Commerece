
using Domain.Contracts;
using E_Commerece.Extensions;
using E_Commerece.Factories;
using E_Commerece.MiddleWare;
using Microsoft.AspNetCore.Mvc;
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

			builder.Services.AddInfraStructureSevice(builder.Configuration);
			builder.Services.AddCoreServices(builder.Configuration);
			builder.Services.AddPresentaionServices();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

			var app = builder.Build();
			await app.SeedDataBaseAsync();
			app.UseMiddleware<GlobalErrorHandlingMiddleWare>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStaticFiles();

			app.UseHttpsRedirection();
			app.UseAuthentication();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();

		}
	}
}
