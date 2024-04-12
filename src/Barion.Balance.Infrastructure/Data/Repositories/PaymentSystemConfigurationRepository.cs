using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barion.Balance.Infrastructure.Data.Repositories;

public class PaymentSystemConfigurationRepository(IBalanceDbContext context)
    : BaseRepository<PaymentSystemConfiguration>(context), IPaymentSystemConfigurationRepository
{
    public async Task<PaymentSystemConfiguration?> GetByPaymentSystemName(string name, CancellationToken cancellationToken = default)
    {
        return await dbSet.SingleOrDefaultAsync(x => x.PaymentSystemName == name, cancellationToken);
    }
}