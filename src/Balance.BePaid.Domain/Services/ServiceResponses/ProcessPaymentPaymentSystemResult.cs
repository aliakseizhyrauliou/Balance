using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Domain.Services.ServiceResponses;

public class ProcessPaymentPaymentSystemResult
{
    public bool IsOk { get; set; }
    public string? PaymentSystemTransactionId { get; set; }
    public Payment? Payment { get; set; }
    public string ErrorMessage { get; set; }
    public string FriendlyErrorMessage { get; set; }
}