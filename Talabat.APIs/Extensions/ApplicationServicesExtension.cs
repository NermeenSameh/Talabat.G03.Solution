using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.Core;
using Route.Talabat.Core.Entities.Identity;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Services.Contract;
using Route.Talabat.Infrastructure;
using Route.Talabat.Infrastructure._Identity;
using Route.Talabat.Service.AuthService;
using Route.Talabat.Service.CacheService;
using Route.Talabat.Service.OrderService;
using Route.Talabat.Service.PaymentService;
using Route.Talabat.Service.ProductService;
using StackExchange.Redis;
using System.Text;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;

namespace Talabat.APIs.Extensions
{
	public static class ApplicationServicesExtension
	{

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddSingleton(typeof(IResponseCacheService), typeof(ResponseCacheService));
		
			services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

			services.AddScoped(typeof(IProductService), typeof(ProductService));
			services.AddScoped(typeof(IOrderService), typeof(OrderService));

			services.AddScoped(typeof(IUniteOfWork), typeof(UniteOfWork));

			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			// webApplicationBuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

			services.AddAutoMapper(typeof(MappingProfiles));

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
														 .SelectMany(P => P.Value.Errors)
														 .Select(E => E.ErrorMessage)
														 .ToArray();
					var response = new ApiValidationErrorResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);
				};
			});

			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));



			return services;

		}


		public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
				{
					//options.Password.RequiredUniqueChars = 2;
					//options.Password.RequireDigit= true;
					//options.Password.RequireLowercase= true;
					//options.Password.RequireUppercase= true;
				})
					.AddEntityFrameworkStores<ApplicationIdentityDbContext>();


			services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/ options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer = configuration["JWT:ValidIssuer"],
						ValidateAudience = true,
						ValidAudience = configuration["JWT:ValidAudience"],
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
						ValidateLifetime = true,
						ClockSkew = TimeSpan.Zero,
					};

				});

			services.AddScoped(typeof(IAuthService), typeof(AuthService));

			return services;
		}


	}
}
