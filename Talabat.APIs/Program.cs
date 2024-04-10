
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Infrastructure.Data;

namespace Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			/// StoreContext dbContext = /*new StoreContext()*/;
			/// await dbContext.Database.MigrateAsync();


			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region Configure Services

			// Add services to the container.

			webApplicationBuilder.Services.AddControllers();
			// Register Required Web APIs Services to the DI Container

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();

			webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			});

			#endregion

			var app = webApplicationBuilder.Build();

			using var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;

			var _dbContext = services.GetRequiredService<ApplicationDbContext>();
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



			#region Configure Kestrel Middlewares

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			#endregion

			app.Run();
		}
	}
}
