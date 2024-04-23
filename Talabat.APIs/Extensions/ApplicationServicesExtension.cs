using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure;
using StackExchange.Redis;
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

	}
}
