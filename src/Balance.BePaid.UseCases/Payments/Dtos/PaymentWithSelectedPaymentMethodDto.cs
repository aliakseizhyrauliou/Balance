namespace Balance.BePaid.UseCases.Payments.Dtos;

public record PaymentWithSelectedPaymentMethodDto
{
    public required string PaidResourceId { get; set; }
    
    public required string OperatorId { get; set; }
    
    public required decimal Amount { get; set; }

    public required int PaidResourceTypeId { get; set; }
    
    public Dictionary<string, string>? AdditionalData { get; set; }
    
    public bool WriteToDebtorsInCaseFail { get; set; }

    public required int PaymentSystemConfigurationId { get; set; }
}