using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Services;

public interface IPaymentSystemService
{
    Task<string> GeneratePaymentSystemWidget(PaymentSystemWidgetGeneration paymentSystemWidgetGeneration,
        CancellationToken cancellationToken);
    
    Task<Hold> MakeHold(Hold hold,
        CancellationToken cancellationToken);

    Task<bool> CaptureHold(Hold hold, 
        CancellationToken cancellationToken);

    Task<bool> VoidHold(Hold hold,
        CancellationToken cancellationToken);
}