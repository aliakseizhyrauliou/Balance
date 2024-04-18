using Newtonsoft.Json;

namespace Barion.Balance.UseCases.PaymentSystemWidgets.Dtos;

public record GeneratePaymentMethodWidgetDto
{
    public required string OperatorId { get; set; }

    public string? PaidResourceId { get; set; }
    
    [JsonProperty]
    public Dictionary<string, string>? AdditionalData { get; set; }
    
    public required int PaidResourceTypeId { get; set; }
}