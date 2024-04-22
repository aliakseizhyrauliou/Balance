using Newtonsoft.Json;

namespace Balance.BePaid.UseCases.PaymentSystemWidgets.Dtos;

public class GeneratePaymentWidgetDto
{
    public required string OperatorId { get; set; }
    public required string PaidResourceId { get; set; }

    public required decimal Amount { get; set; }

    [JsonProperty]
    public Dictionary<string, string>? AdditionalData { get; set; }
    
    public required int PaidResourceTypeId { get; set; }
    
    public required int PaymentSystemConfigurationId { get; set; }
}