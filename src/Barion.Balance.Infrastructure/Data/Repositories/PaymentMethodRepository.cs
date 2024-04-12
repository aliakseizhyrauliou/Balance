using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barion.Balance.Infrastructure.Data.Repositories;

public sealed class PaymentMethodRepository(IBalanceDbContext context)
    : BaseRepository<PaymentMethod>(context), IPaymentMethodRepository
{
    public async Task UnselectAllPaymentMethodsAsync(string userId, 
        CancellationToken cancellationToken = default)
    {
        await dbSet.Where(x => x.UserId == userId)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsSelected, false), cancellationToken);
    }
}