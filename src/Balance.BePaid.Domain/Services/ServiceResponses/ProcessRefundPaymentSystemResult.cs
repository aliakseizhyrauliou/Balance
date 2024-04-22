using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Domain.Services.ServiceResponses;

public class ProcessRefundPaymentSystemResult
{
    public bool IsOk { get; set; }
    public Payment? Payment { get; set; }
    
    public string? RefundPaymentUrl { get; set; }
    public string ErrorMessage { get; set; }
    public string FriendlyErrorMessage { get; set; }
}