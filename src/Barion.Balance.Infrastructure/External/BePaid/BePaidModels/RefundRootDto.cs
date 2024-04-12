using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels;

public class RefundRootDto
{
    [JsonProperty(PropertyName = "request")]
    public RefundDto Request { get; set; }
}


public class RefundDto
{
    [JsonProperty(PropertyName = "parent_uid")]
    public string ParentUid { get; set; }
    
    [JsonProperty(PropertyName = "amount")]
    public int Amount { get; set; }
    
    [JsonProperty(PropertyName = "reason")]
    public string Reason { get; set; }

    [JsonProperty(PropertyName = "credit_card")]
    public CreditCardDto CreditCard { get; set; }
}

public class CreditCardDto
{
    [JsonProperty(PropertyName = "token")]
    public string Token { get; set; }
}