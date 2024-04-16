using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Services.ServiceResponses;

public class ProcessCreatePaymentMethodPaymentSystemWidgetResult
{
    public bool IsOk { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public PaymentSystemWidgetGeneration PaymentSystemWidgetGeneration { get; set; } = null!;
    public string ErrorMessage { get; set; }
    public string FriendlyErrorMessage { get; set; }
}