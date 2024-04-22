using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Customer;

public class Customer
{
    [JsonProperty(PropertyName = "first_name")]
    public required string FirstName { get; set; }
    
    [JsonProperty(PropertyName = "last_name")]
    public required string LastName { get; set; }

}