using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Services;

public interface IPaymentSystemConfigurationService
{
    Task<PaymentSystemConfiguration?> GetPaymentSystemConfiguration(string paymentSystemName, CancellationToken cancellationToken = default);
}