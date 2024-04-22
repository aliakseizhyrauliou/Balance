using Balance.BePaid.Application.Common.Interfaces;
using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Infrastructure.Data.Repositories;

public class DebtorRepository(IBalanceDbContext context) : BaseRepository<Debtor>(context), IDebtorRepository
{
}