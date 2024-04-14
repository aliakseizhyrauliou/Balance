using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus
{
    public class TransactionStatus
    {
        public const string Successful = "successful";
        public const string Failed = "failed";
        public const string Incomplete = "incomplete";
        public const string Expired = "expired";
    }
}
    