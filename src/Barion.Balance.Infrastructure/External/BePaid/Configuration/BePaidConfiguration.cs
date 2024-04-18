using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request;

namespace Barion.Balance.Infrastructure.External.BePaid.Configuration;

public class BePaidConfiguration
{
    public required Urls Urls { get; set; } 
    public required Checkout CheckoutAuthorization { get; set; }
    public required Checkout CheckoutPayment { get; set; }
    public required Checkout CheckoutCreatePaymentMethod { get; set; }
    public required string DefaultCurrency { get; set; }
    public required string DefaultLanguage { get; set; }
}


public sealed class Urls
{
    public required UrlWithDescription CheckoutUrl { get; set; }
    public required UrlWithDescription NotificationUrl { get; set; }
    public required UrlWithDescription RefundUrl { get; set; }
    public required UrlWithDescription AuthorizationUrl { get; set; }
    public required UrlWithDescription PaymentUrl { get; set; }
    public required UrlWithDescription CaptureHoldUrl { get; set; }
    public required UrlWithDescription VoidHold { get; set; }

    public class UrlWithDescription
    {
        public string? Description { get; set; }
        public required string Url { get; set; }
    }
}





