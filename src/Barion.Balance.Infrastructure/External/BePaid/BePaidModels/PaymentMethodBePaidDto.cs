using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels;

public class PaymentMethodBePaidDto
{
    [JsonProperty(PropertyName= "types")]
    public List<string> Types { get; set; }
}