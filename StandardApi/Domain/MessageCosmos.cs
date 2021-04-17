using Cosmonaut.Attributes;
using Newtonsoft.Json;

namespace StandardApi.Domain
{
    [CosmosCollection("messages")]
    public class MessageCosmos
    {
        [CosmosPartitionKey]
        [JsonProperty("id")]
        public string Id { get; set; }

        public string Text { get; set; }
    }
}
