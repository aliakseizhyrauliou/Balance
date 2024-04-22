using Newtonsoft.Json;

namespace Balance.BePaid.UseCases.PaymentSystemWidgets.Dtos;

public record GeneratePaymentMethodWidgetDto
{
    public required string OperatorId { get; set; }

    public string? PaidResourceId { get; set; }
    
    [JsonProperty]
    public Dictionary<string, string>? AdditionalData { get; set; }
    
    public required int PaidResourceTypeId { get; set; }

    public required int PaymentSystemConfigurationId { get; set; }
}