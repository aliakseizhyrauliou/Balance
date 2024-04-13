using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Settings;

public class Settings
{
    [JsonProperty(PropertyName = "button_text")] 
    public required string ButtonText { get; set; }

    [JsonProperty(PropertyName = "language")] 
    public required string Language { get; set; }

    [JsonPropertyName("notification_url")]
    public required string NotificationUrl { get; set; }

    [JsonProperty(PropertyName = "save_card_toggle")] 
    public required SaveCardToggle SaveCardToggle { get; set; }

    [JsonPropertyName("customer_fields")]
    public required CustomerFields CustomerFields { get; set; }
}