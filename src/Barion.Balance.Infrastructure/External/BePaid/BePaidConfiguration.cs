namespace Barion.Balance.Infrastructure.External.BePaid;

public class BePaidConfiguration
{
    public Urls Urls { get; set; } = null!;
    public GenerateCardToken GenerateCardToken { get; set; } = null!;
}


public sealed class Urls
{
    public UrlWithDescription CheckoutUrl { get; set; }
    public UrlWithDescription NotificationUrl { get; set; }
    public UrlWithDescription RefundUrl { get; set; }
    public UrlWithDescription AuthorizationUrl { get; set; }
    public UrlWithDescription PaymentUrl { get; set; }
    public UrlWithDescription CaptureHoldUrl { get; set; }
    public UrlWithDescription VoidHold { get; set; }

    public class UrlWithDescription
    {
        public string? Description { get; set; }
        public required string Url { get; set; }
    }
}

/// <summary>
/// Когфигурация для создания токена карты
/// </summary>
public class GenerateCardToken
{
    public bool IsTest { get; set; }
    public string TransactionType { get; set; } = null!;
    public int AttemptsCount { get; set; }

    public GenerateCardTokenSettings Settings { get; set; } = null!;

    public GenerateCardTokenOrder Order { get; set; } = null!;

    public class GenerateCardTokenSettings
    {
        public string DefaultButtonText { get; set; } = null!;
        public string DefaultLanguage { get; set; } = null!;
    }
    
    public class GenerateCardTokenOrder
    {
        public string DefaultCurrency = null!;
        public int DefaultAmount { get; set; }

        public string DefaultDescription { get; set; } = null!;

        public GenerateCardTokenAdditionalData? AdditionalData { get; set; }

        public class GenerateCardTokenAdditionalData
        {
            public List<string>? Contract { get; set; }
        }
    }
}






