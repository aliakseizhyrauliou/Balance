using Balance.BePaid.Application.Common.Interfaces;
using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Balance.BePaid.Infrastructure.Data.Repositories;

public class PaymentSystemWidgetGenerationRepository(IBalanceDbContext context) : BaseRepository<PaymentSystemWidget>(context), 
    IPaymentSystemWidgetGenerationRepository
{
    public async Task<PaymentSystemWidget?> GetActiveAsync(string userId, 
        CancellationToken cancellationToken = default)
    {
        return await dbSet.SingleOrDefaultAsync(x => x.UserId == userId &&
                                                     !x.GotResponseFromPaymentSystem &&
                                                     !x.IsDisabled, cancellationToken);
    }

    public async Task DisableAllUserWidgetsAsync(string userId,
        CancellationToken cancellationToken = default)
    {
        await dbSet
            .Where(x => x.UserId == userId && !x.GotResponseFromPaymentSystem)
            .ExecuteUpdateAsync(x 
                => x.SetProperty(paymentSystemWidgetGeneration => paymentSystemWidgetGeneration.IsDisabled, true), cancellationToken);
    }
}