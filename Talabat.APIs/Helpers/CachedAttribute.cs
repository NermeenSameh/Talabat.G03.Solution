using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Route.Talabat.Core.Services.Contract;
using System.Text;

namespace Talabat.APIs.Helpers
{
	public class CachedAttribute : Attribute, IAsyncActionFilter
	{
		private readonly int _timeToLiveInSecond;

		public CachedAttribute(int timeToLiveInSecond)
		{
			_timeToLiveInSecond = timeToLiveInSecond;
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var responseCacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
			// Ask CLR for Creating Object from "ResponseCachingService" Explicity

			var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

			var response = await responseCacheService.GetCachedResponseAsync(cacheKey);

			if (!string.IsNullOrEmpty(response))
			{
				var result = new ContentResult()
				{
					Content = response,
					ContentType = "application/json",
					StatusCode = 200,

				};
				context.Result = result;
				return;
			}

			var excutedActionContext = await next.Invoke(); // will Excute the Next Action Filter or the Action itself

            if (excutedActionContext.Result is OkObjectResult okObjectResult && okObjectResult.Value is not null)
            {
				await responseCacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSecond));

			}

        }

		private string GenerateCacheKeyFromRequest(HttpRequest request)
		{
			var keyBuilder = new StringBuilder();

			keyBuilder.Append(request.Path);

			foreach (var (key , value) in request.Query.OrderBy(x => x.Key))
			{
				keyBuilder.Append($"|{key}-{value}");
			}

			return keyBuilder.ToString();
		}
	}
}
