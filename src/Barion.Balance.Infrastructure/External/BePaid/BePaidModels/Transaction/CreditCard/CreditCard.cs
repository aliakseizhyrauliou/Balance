using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction.CreditCard
{
    public class CreditCard
    {
        [JsonProperty(PropertyName = "brand")]
        public required string Brand { get; set; }

        [JsonProperty(PropertyName = "product")]
        public required string Product { get; set; }

        [JsonProperty(PropertyName = "sub_brand")]
        public string? SubBrand {  get; set; }

        [JsonProperty(PropertyName = "last_4")]
        public required string Last4 { get; set; }

        [JsonProperty(PropertyName = "first_1")]
        public required string First1 { get; set; }

        /// <summary>
        /// Шестизначный банковский идентификационный номер. Первые 6 цифр номера карты.
        /// </summary>
        [JsonProperty(PropertyName = "bin")]
        public required string Bin { get; set;}


        /// <summary>
        /// Восьмизначный банковский идентификационный номер. Первые 8 цифр номера карты.
        /// </summary>
        [JsonProperty(PropertyName = "bin_8")]
        public required string Bin8 { get; set; }


        /// <summary>
        /// Страна банка, выпустившего карту в формате ISO 3166-1 alpha-2.
        /// </summary>
        [JsonProperty(PropertyName = "issuer_country")]
        public required string IssuerCountry { get; set; }


        /// <summary>
        /// Название банка, выпустившего карту.
        /// </summary>
        [JsonProperty(PropertyName = "issuer_name")]
        public required string IssuerName { get; set; }

        /// <summary>
        /// Хэш карты. Постоянная величина, даже если дата окончания действия карты или владелец изменены.
        /// </summary>
        [JsonProperty(PropertyName = "stamp")]
        public required string Stamp { get; set; }

        /// <summary>
        /// Токен карты. Позволяет сохранять данные покупателей и производить оплату, когда они делают покупку или вы возобновляете свои услуги.
        /// </summary>
        [JsonProperty(PropertyName = "token")]
        public required string Token { get; set; }

        /// <summary>
        /// В классе TokenProviders
        /// </summary>
        [JsonProperty(PropertyName = "token_provider")]
        public string? TokenProvider { get; set; }

        /// <summary>
        /// Ссылка на квитанцию обработанной транзакции
        /// </summary>
        [JsonProperty(PropertyName = "receipt_url")]
        public required string ReceiptUrl { get; set; }


        [JsonProperty(PropertyName = "exp_month")]
        public required int ExpMonth { get; set; }

        [JsonProperty(PropertyName = "exp_year")]
        public int ExpYear { get; set; }
    }
}
