using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services.ServiceResponses;

namespace Barion.Balance.Domain.Services;

public interface IPaymentSystemService
{
    Task<int> GetWidgetId(string jsonResponse, CancellationToken cancellationToken);
    Task<string> GeneratePaymentSystemWidget(PaymentSystemWidgetGeneration paymentSystemWidgetGeneration,
        CancellationToken cancellationToken);

    Task<ProcessCreatePaymentMethodPaymentSystemWidgetResult> ProcessCreatePaymentMethodPaymentSystemWidgetResponse(string jsonResponse,
        PaymentSystemWidgetGeneration widgetGeneration,
        CancellationToken cancellationToken = default);
    Task<Hold> MakeHold(Hold hold,
        CancellationToken cancellationToken);

    Task<bool> CaptureHold(Hold hold, 
        CancellationToken cancellationToken);

    Task<bool> VoidHold(Hold hold,
        CancellationToken cancellationToken);
}