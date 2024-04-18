using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Infrastructure.Data.Repositories;

public class ReceiptRepository(IBalanceDbContext context) : BaseRepository<Receipt>(context), IReceiptRepository;