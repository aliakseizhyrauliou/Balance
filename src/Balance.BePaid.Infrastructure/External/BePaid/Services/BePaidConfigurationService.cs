using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Services;

namespace Balance.BePaid.Infrastructure.External.BePaid.Services;

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