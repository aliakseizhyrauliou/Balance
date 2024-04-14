using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction.CreditCard.AdditionalData
{
    public class Params
    {

        /// <summary>
        /// ID пользовательской сессии.
        /// </summary>
        [JsonProperty(PropertyName = "session")]
        public string Session { get; set; }
    }
}
