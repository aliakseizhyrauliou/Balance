using Newtonsoft.Json;

namespace Barion.Balance.UseCases.Holds.Dtos;

public record HoldWithSelectedPaymentMethodDto
{
    public required string PaidResourceId { get; set; }
    
    public required string OperatorId { get; set; }
    
    public required decimal Amount { get; set; }

    public required int PaidResourceTypeId { get; set; }
    
    [JsonProperty]
    public Dictionary<string, string>? AdditionalData { get; set; }
    
    public required int PaymentSystemConfigurationId { get; set; }
}