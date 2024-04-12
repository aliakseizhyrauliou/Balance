using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels;

public class CustomerFieldsDto
{
    [JsonProperty(PropertyName ="visible")]
    public List<string> Visible { get; set; }
    
    [JsonProperty(PropertyName = "read_only")]
    public List<string> ReadOnly { get; set; }
}