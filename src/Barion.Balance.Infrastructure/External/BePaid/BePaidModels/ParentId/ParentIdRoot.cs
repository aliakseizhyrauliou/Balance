using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.ParentId;

public class ParentIdRoot
{
    [JsonProperty(PropertyName = "request")]
    public required ParentIdRequest Request { get; set; }
}

public class ParentIdRequest
{
    [JsonProperty(PropertyName = "parent_uid")]
    public required string ParentId { get; set; }

    [JsonProperty(PropertyName = "amount")]
    public required int Amount { get; set; }

    [JsonProperty(PropertyName = "reason")]
    public string? Reason { get; set; }
}