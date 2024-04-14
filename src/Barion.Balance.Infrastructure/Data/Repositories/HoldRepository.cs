using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Infrastructure.Data.Repositories;

public class HoldRepository(IBalanceDbContext context) : BaseRepository<Hold>(context), IHoldRepository;