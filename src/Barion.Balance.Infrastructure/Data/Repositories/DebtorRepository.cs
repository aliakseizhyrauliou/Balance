using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Infrastructure.Data.Repositories;

public class DebtorRepository(IBalanceDbContext context) : BaseRepository<Debtor>(context), IDebtorRepository
{
}