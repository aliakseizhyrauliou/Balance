using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.CreateTransaction;

public class CreateTransactionCreditCard
{
    [JsonProperty(PropertyName = "token")]
    public required string Token { get; set; }

}