using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels;

public class CreditCardFieldsDto
{
    [JsonProperty(PropertyName = "holder")]
    public string Holder { get; set; }
    
    [JsonProperty(PropertyName = "read_only")]
    public List<string> ReadOnly { get; set; }
}