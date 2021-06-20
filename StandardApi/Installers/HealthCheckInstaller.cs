using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StandardApi.Data;
using StandardApi.HealthCheckUtil;

namespace StandardApi.Installers
{
    public class HealthCheckInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                     .AddDbContextCheck<DataContext>()
                     .AddCheck<RedisHealthCheck>("Redis");
        }
    }
}
