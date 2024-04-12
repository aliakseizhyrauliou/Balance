using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels;

public class AdditionalDataDto
{
    [JsonProperty(PropertyName = "contract")]
    public List<string> Contract { get; set; }

}