using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using StandardApi.Options;
using StandardApi.Services;

namespace StandardApi.Installers
{
    public class CacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisCachingSettings = new RedisCachingSettings();
            configuration.GetSection(nameof(RedisCachingSettings)).Bind(redisCachingSettings);
            services.AddSingleton(redisCachingSettings);

            if (!redisCachingSettings.Enabled)
            {
                return;
            }
            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisCachingSettings.ConnectionString));
            services.AddStackExchangeRedisCache(options => options.Configuration = redisCachingSettings.ConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
