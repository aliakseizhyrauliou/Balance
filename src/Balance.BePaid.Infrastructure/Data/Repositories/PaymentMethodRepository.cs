using Balance.BePaid.Application.Common.Interfaces;
using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Balance.BePaid.Infrastructure.Data.Repositories;

public sealed class PaymentMethodRepository(IBalanceDbContext context)
    : BaseRepository<PaymentMethod>(context), IPaymentMethodRepository
{
    public async Task<PaymentMethod?> GetSelectedAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await dbSet.SingleOrDefaultAsync(x => x.UserId == userId && x.IsSelected, cancellationToken);
    }

    public async Task UnselectAllPaymentMethodsAsync(string userId, 
        CancellationToken cancellationToken = default)
    {
        await dbSet.Where(x => x.UserId == userId)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsSelected, false), cancellationToken);
    }
}