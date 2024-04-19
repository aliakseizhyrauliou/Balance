using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Application.Common.Repositories;

public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod>
{
    Task<PaymentMethod?> GetSelectedAsync(string userId, CancellationToken cancellationToken = default);
    Task UnselectAllPaymentMethodsAsync(string userId, CancellationToken cancellationToken = default);
}