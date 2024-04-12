using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels;

public class CustomerDto
{
    [JsonProperty(PropertyName= "address")]
    public string Address { get; set; }
    
    [JsonProperty(PropertyName= "country")]
    public string Country { get; set; }
    
    [JsonProperty(PropertyName = "city")]
    public string City { get; set; }
    
    [JsonProperty(PropertyName ="email")]
    public string Email { get; set; }
}