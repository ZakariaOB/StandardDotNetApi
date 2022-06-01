
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using StandardApi.Options;
using StandardApi.Services;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace StandardApi.Cache
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLeaveInSeconds;
        
        public CachedAttribute(int timeToLeaveInSeconds)
        {
            _timeToLeaveInSeconds = timeToLeaveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Before (Going to the controller)
            var redisCachingSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCachingSettings>();
            if (!redisCachingSettings.Enabled)
            {
                await next();
                return;
            }

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLeaveInSeconds));
            }
        }

        private static string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
