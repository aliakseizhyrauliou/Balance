using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.CreateTransaction;

public class CreateTransactionRoot
{
    [JsonProperty(PropertyName = "request")]
    public CreateTransaction Request { get; set; }
}