using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction
{
    public class TransactionRoot
    {
        [JsonProperty(PropertyName = "transaction")]
        public Transaction Transaction { get; set; }
    }
}
