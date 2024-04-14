using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barion.Balance.Infrastructure.Data.Repositories;

public class PaymentSystemWidgetGenerationRepository(IBalanceDbContext context) : BaseRepository<PaymentSystemWidgetGeneration>(context), 
    IPaymentSystemWidgetGenerationRepository
{
    public async Task<PaymentSystemWidgetGeneration?> GetActiveAsync(string userId, 
        CancellationToken cancellationToken = default)
    {
        return await dbSet.SingleOrDefaultAsync(x => x.UserId == userId &&
                                                     !x.IsCompleted &&
                                                     !x.IsDisabled, cancellationToken);
    }

    public async Task DisableAllUserWidgetsAsync(string userId,
        CancellationToken cancellationToken = default)
    {
        await dbSet
            .Where(x => x.UserId == userId && !x.IsCompleted)
            .ExecuteUpdateAsync(x 
                => x.SetProperty(paymentSystemWidgetGeneration => paymentSystemWidgetGeneration.IsDisabled, true), cancellationToken);
    }
}