using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Services;

public interface IPaymentSystemService
{
    Task<int> GetWidgetId(string jsonResponse, CancellationToken cancellationToken);
    Task<string> GeneratePaymentSystemWidget(PaymentSystemWidgetGeneration paymentSystemWidgetGeneration,
        CancellationToken cancellationToken);

    Task<PaymentMethod> ProcessCreatePaymentMethodPaymentSystemWidgetResponse(string jsonResponse,
        PaymentSystemWidgetGeneration widgetGeneration,
        CancellationToken cancellationToken = default);
    Task<Hold> MakeHold(Hold hold,
        CancellationToken cancellationToken);

    Task<bool> CaptureHold(Hold hold, 
        CancellationToken cancellationToken);

    Task<bool> VoidHold(Hold hold,
        CancellationToken cancellationToken);
}