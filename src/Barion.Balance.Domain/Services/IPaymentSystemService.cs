using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services.Models;

namespace Barion.Balance.Domain.Services;

public interface IPaymentSystemService
{
    Task<Hold> MakeHold(MakeHold makeHold,
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken);

    Task<bool> CaptureHold(Hold hold, 
        CancellationToken cancellationToken);

    Task<bool> VoidHold(Hold hold,
        CancellationToken cancellationToken);
}