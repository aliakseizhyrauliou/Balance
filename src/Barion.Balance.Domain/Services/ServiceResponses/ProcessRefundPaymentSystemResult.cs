using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Services.ServiceResponses;

public class ProcessRefundPaymentSystemResult
{
    public bool IsOk { get; set; }
    public Payment? Payment { get; set; }
    
    public string? RefundPaymentUrl { get; set; }
    public string ErrorMessage { get; set; }
    public string FriendlyErrorMessage { get; set; }
}