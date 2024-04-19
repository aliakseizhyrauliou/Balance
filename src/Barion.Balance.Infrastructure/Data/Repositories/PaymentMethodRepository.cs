using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barion.Balance.Infrastructure.Data.Repositories;

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