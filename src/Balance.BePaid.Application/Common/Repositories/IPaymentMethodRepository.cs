using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Application.Common.Repositories;

public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod>
{
    Task<PaymentMethod?> GetSelectedAsync(string userId, CancellationToken cancellationToken = default);
    Task UnselectAllPaymentMethodsAsync(string userId, CancellationToken cancellationToken = default);
}