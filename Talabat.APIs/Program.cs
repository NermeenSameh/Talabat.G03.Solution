
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure;
using Route.Talabat.Infrastructure.Data;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;

namespace Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{

			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region Configure Services

			// Add services to the container.

			webApplicationBuilder.Services.AddControllers();

			webApplicationBuilder.Services.AddSwaggerServices();

			webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			});

			webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
			{
				var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");

				return ConnectionMultiplexer.Connect(connection);
			});

			webApplicationBuilder.Services.AddApplicationServices();





			#endregion

			var app = webApplicationBuilder.Build();

			#region Apply All Pending Migrations [Update-Database] and Data Seeding
			using var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;

			var _dbContext = services.GetRequiredService<StoreContext>();
			// ASK CLR for Creating Object from DbContext Explicitly
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{

				await _dbContext.Database.MigrateAsync();
				await StoreContextSeed.SeedAsync(_dbContext);
			}
			catch (Exception ex)
			{

				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An Error has been occured during apply the migration");
			}
			#endregion



			#region Configure Kestrel Middlewares

			app.UseMiddleware<ExceptionMiddleware>();

			if (app.Environment.IsDevelopment())
			{

				app.UseSwaggerMiddleware();

				// app.UseDeveloperExceptionPage();

			}

			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseStaticFiles();


			app.MapControllers();

			#endregion

			app.Run();
		}
	}
}
