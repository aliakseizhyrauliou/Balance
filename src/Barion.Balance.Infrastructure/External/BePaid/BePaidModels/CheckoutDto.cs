using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels
{
    public class CheckoutDto
    {
        [JsonProperty(PropertyName = "test")]
        public bool Test { get; set; }
        
        [JsonProperty(PropertyName = "transaction_type")]
        public string TransactionType { get; set; }
        
        [JsonProperty(PropertyName = "attempts")]
        public int Attempts { get; set; }
        
        [JsonProperty(PropertyName = "settings")]
        public SettingsDto Settings { get; set; }
        
        [JsonProperty( PropertyName= "payment_method")]
        public PaymentMethodBePaidDto PaymentMethodBePaid { get; set; }
        
        [JsonProperty(PropertyName = "order")]
        public OrderDto Order { get; set; }
        
        [JsonProperty(PropertyName = "customer")]
        public CustomerDto CustomerDto { get; set; }
    }
}
