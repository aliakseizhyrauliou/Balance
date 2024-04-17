using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Services.ServiceResponses;

public class ProcessCaptureHoldPaymentSystemResult
{
    public bool IsOk { get; set; }
    public string? PaymentSystemTransactionId { get; set; }

    public bool NeedToCreateAccountRecord { get; set; }

    public AccountRecord? AccountRecord { get; set; }
    public Hold? Hold { get; set; }
    public string ErrorMessage { get; set; }
    public string FriendlyErrorMessage { get; set; }
    
}