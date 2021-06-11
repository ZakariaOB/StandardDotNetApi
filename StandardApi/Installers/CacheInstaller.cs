using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddStackExchangeRedisCache(options => options.Configuration = redisCachingSettings.ConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
