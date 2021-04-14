using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace StandardApi.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installersClasses = typeof(Startup).Assembly.ExportedTypes
                    .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance)
                    .Cast<IInstaller>()
                    .ToList();

            installersClasses.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
