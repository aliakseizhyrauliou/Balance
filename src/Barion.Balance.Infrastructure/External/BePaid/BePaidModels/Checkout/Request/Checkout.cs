using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request;

public class Checkout
{
    [JsonProperty(PropertyName = "test")]
    public required bool Test { get; set; }
        
    [JsonProperty(PropertyName = "transaction_type")]
    public required string TransactionType { get; set; }
        
    [JsonProperty(PropertyName = "attempts")]
    public int Attempts { get; set; }
        
    [JsonProperty( PropertyName= "payment_method")]
    public required PaymentMethod.PaymentMethod PaymentMethod { get; set; }
        
    [JsonProperty(PropertyName = "order")]
    public required Order.Order Order { get; set; }

    [JsonProperty(PropertyName = "settings")] 
    public required Settings.Settings Settings { get; set; }

    [JsonProperty(PropertyName = "customer")]
    public required Customer.Customer Customer { get; set; }
}