using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StandardApi.Domain;
using StandardApi.Options;

namespace StandardApi.Installers
{
    public class CosmoInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var cosmosSettings = new CosmosSettings();
            configuration.GetSection(nameof(CosmosSettings)).Bind(cosmosSettings);

            var cosmosStoreSettings = new CosmosStoreSettings
            (
                cosmosSettings.DatabaseName,
                cosmosSettings.AccountUri,
                cosmosSettings.AccountKey,
                new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Tcp }
            );

            services.AddCosmosStore<MessageCosmos>(cosmosStoreSettings);
        }
    }
}
