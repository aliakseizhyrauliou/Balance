using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction.CreditCard.AdditionalData
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
