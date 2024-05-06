using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.Core;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Services.Contract;
using Route.Talabat.Infrastructure;
using Route.Talabat.Service.AuthService;
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


		public static IServiceCollection AddAuthServices(this IServiceCollection services , IConfiguration configuration)
		{
			services.AddScoped(typeof(IUniteOfWork) , typeof(UniteOfWork));

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
