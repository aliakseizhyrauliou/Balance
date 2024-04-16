using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels;

public class BePaidErrorRoot
{
    [JsonProperty(PropertyName = "response")]
    public string Response { get; set; }
}

public class BePaidErorr
{
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }
}

