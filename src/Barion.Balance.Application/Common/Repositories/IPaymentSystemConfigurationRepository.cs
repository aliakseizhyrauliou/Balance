using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Application.Common.Repositories;

public interface IPaymentSystemConfigurationRepository : IBaseRepository<PaymentSystemConfiguration>
{
    Task<PaymentSystemConfiguration?> GetCurrentSchemaAsync(CancellationToken cancellationToken = default);
    Task<PaymentSystemConfiguration?> GetByPaymentSystemName(string name, CancellationToken cancellationToken = default);
}