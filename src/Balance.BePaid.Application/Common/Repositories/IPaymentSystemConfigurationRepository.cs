using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Application.Common.Repositories;

public interface IPaymentSystemConfigurationRepository : IBaseRepository<PaymentSystemConfiguration>
{
    Task<PaymentSystemConfiguration?> GetCurrentSchemaAsync(CancellationToken cancellationToken = default);
    Task<PaymentSystemConfiguration?> GetByPaymentSystemName(string name, CancellationToken cancellationToken = default);
}