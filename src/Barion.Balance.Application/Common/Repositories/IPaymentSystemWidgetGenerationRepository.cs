using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Application.Common.Repositories;

public interface IPaymentSystemWidgetGenerationRepository : IBaseRepository<PaymentSystemWidgetGeneration>
{
    Task<PaymentSystemWidgetGeneration?> GetActiveAsync(string userId,
        CancellationToken cancellationToken = default);
    Task DisableAllUserWidgetsAsync(string userId,
        CancellationToken cancellationToken = default);
}