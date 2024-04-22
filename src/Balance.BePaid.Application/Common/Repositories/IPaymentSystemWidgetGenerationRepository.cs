using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Application.Common.Repositories;

public interface IPaymentSystemWidgetGenerationRepository : IBaseRepository<PaymentSystemWidget>
{
    Task<PaymentSystemWidget?> GetActiveAsync(string userId,
        CancellationToken cancellationToken = default);
    Task DisableAllUserWidgetsAsync(string userId,
        CancellationToken cancellationToken = default);
}