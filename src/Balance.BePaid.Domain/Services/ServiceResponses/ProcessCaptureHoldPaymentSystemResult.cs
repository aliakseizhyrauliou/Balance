using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Domain.Services.ServiceResponses;

public class ProcessCaptureHoldPaymentSystemResult
{
    public bool IsOk { get; set; }
    public string? PaymentSystemTransactionId { get; set; }

    public bool NeedToCreatePaymentRecord { get; set; }

    public Payment? Payment { get; set; }
    public Hold? Hold { get; set; }
    public string ErrorMessage { get; set; }
    public string FriendlyErrorMessage { get; set; }
    
}