using System.Text.Json.Serialization;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Customer;

public class Customer
{
    [JsonPropertyName("first_name")]
    public required string FirstName { get; set; }
    
    [JsonPropertyName("last_name")]
    public required string LastName { get; set; }

}