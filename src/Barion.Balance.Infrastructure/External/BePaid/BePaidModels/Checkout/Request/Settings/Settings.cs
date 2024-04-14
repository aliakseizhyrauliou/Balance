using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Settings;

public class Settings
{
    [JsonProperty(PropertyName = "button_text")] 
    public required string ButtonText { get; set; }

    [JsonProperty(PropertyName = "language")] 
    public required string Language { get; set; }
    
    [JsonProperty(PropertyName = "notification_url")] 
    public required string NotificationUrl { get; set; }

    [JsonProperty(PropertyName = "save_card_toggle")] 
    public required SaveCardToggle SaveCardToggle { get; set; }

    [JsonProperty(PropertyName = "customer_fields")] 
    public required CustomerFields CustomerFields { get; set; }
}