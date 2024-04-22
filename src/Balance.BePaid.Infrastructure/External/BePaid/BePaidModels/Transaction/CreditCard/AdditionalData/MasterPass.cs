using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction.CreditCard.AdditionalData
{
    public class MasterPass
    {
        /// <summary>
        /// Секция для параметров Masterpass.
        /// </summary>
        [JsonProperty(PropertyName = "params")]
        public Params? Params { get; set; }

        /// <summary>
        /// Результат транзакции в Masterpass.
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public string? Result { get; set; }
    }
}
