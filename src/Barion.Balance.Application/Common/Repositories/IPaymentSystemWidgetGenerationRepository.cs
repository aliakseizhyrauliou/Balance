using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Application.Common.Repositories;

public interface IPaymentSystemWidgetGenerationRepository : IBaseRepository<PaymentSystemWidget>
{
    Task<PaymentSystemWidget?> GetActiveAsync(string userId,
        CancellationToken cancellationToken = default);
    Task DisableAllUserWidgetsAsync(string userId,
        CancellationToken cancellationToken = default);
}