using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services;
using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid;

public class BePaidConfigurationService(IPaymentSystemConfigurationRepository repository) 
    : IPaymentSystemConfigurationService
{
    public async Task<PaymentSystemConfiguration?> GetPaymentSystemConfiguration(string paymentSystemName, 
        CancellationToken cancellationToken = default)
    {
        var paymentSystemConfiguration = await repository.GetByPaymentSystemName(paymentSystemName, cancellationToken);

        if (paymentSystemConfiguration is null)
        {
            throw new Exception("payment_system_not_found");
        }

        return paymentSystemConfiguration;
    }
}