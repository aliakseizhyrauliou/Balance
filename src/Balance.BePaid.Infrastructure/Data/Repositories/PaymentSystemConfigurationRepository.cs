using Balance.BePaid.Application.Common.Interfaces;
using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Balance.BePaid.Infrastructure.Data.Repositories;

public class PaymentSystemConfigurationRepository(IBalanceDbContext context)
    : BaseRepository<PaymentSystemConfiguration>(context), IPaymentSystemConfigurationRepository
{
    public async Task<PaymentSystemConfiguration?> GetCurrentSchemaAsync(CancellationToken cancellationToken = default)
    {
        return await dbSet.SingleOrDefaultAsync(x => x.IsCurrentSchema, cancellationToken);
    }

    public async Task<PaymentSystemConfiguration?> GetByPaymentSystemName(string name, CancellationToken cancellationToken = default)
    {
        return await dbSet.SingleOrDefaultAsync(x => x.PaymentSystemName == name, cancellationToken);
    }
}