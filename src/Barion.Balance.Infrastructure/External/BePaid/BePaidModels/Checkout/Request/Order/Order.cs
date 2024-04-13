using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Order;

public class Order
{
    [JsonProperty(PropertyName = "currency")]
    public required string Currency { get; set; } 
    
    [JsonProperty(PropertyName= "amount")]
    public required int Amount { get; set; } 
    
    [JsonProperty(PropertyName= "description")]
    public required string Description { get; set; }
    
    [JsonProperty(PropertyName = "additional_data")]
    public required AdditionalData AdditionalData { get; set; }

    [JsonProperty(PropertyName = "tracking_id")] 
    public required string TrackingId { get; set; }
}