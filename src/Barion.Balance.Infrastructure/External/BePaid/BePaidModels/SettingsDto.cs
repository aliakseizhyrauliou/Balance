using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels;

public class SettingsDto
{
    
    [JsonProperty(PropertyName = "button_text")]
    public string ButtonText { get; set; }
    
    [JsonProperty(PropertyName = "button_next_text")]
    public string ButtonNextText { get; set; }
    
    [JsonProperty(PropertyName = "language")]
    public string Language { get; set; }

    [JsonProperty(PropertyName = "success_url")] 
    public string SuccessUrl { get; set; }

    [JsonProperty(PropertyName = "notification_url")] 
    public string NotificationUrl { get; set; }

    [JsonProperty(PropertyName = "customer_fields")]
    public CustomerFieldsDto CustomerFields { get; set; }
    
    [JsonProperty(PropertyName = "credit_card_fields")]
    public CreditCardFieldsDto CreditCardFields { get; set; } 
}