using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.BePaidModels;

public class BePaidErrorsRoot
{
    [JsonProperty(PropertyName = "erorrs")]
    public object Errors { get; set; }
    
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }
}

