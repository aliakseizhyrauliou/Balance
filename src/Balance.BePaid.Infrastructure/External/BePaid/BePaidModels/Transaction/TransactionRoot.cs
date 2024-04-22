using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction
{
    public class TransactionRoot
    {
        [JsonProperty(PropertyName = "transaction")]
        public Transaction Transaction { get; set; }
    }
}
