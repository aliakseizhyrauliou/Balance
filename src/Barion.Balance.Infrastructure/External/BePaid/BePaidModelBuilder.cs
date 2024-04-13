using Barion.Balance.Infrastructure.External.BePaid.BePaidModels;

namespace Barion.Balance.Infrastructure.External.BePaid;

public static class BePaidModelBuilder
{
    public static CheckouRootDto BuildForAuthorizationWithWidget(
        BePaidConfiguration configuration,
        int paymentMethodId)
    {
        return new CheckouRootDto()
        {
            Checkout = new CheckoutDto()
            {
                Test = configuration.GenerateCardToken.IsTest,
                TransactionType = configuration.GenerateCardToken.TransactionType,
                Attempts = configuration.GenerateCardToken.AttemptsCount,
                Settings = new SettingsDto()
                {
                    ButtonText = configuration.GenerateCardToken.Settings.DefaultButtonText,
                    Language = configuration.GenerateCardToken.Settings.DefaultLanguage,
                    NotificationUrl = configuration.Urls.NotificationUrl.Url,
                    CustomerFields = new CustomerFieldsDto()
                    {
                        Visible = ["first_name", "last_name"],
                        ReadOnly = ["email"]
                    },
                    CreditCardFields = new CreditCardFieldsDto()
                    {
                        Holder = "Test test",
                        ReadOnly = ["holder"]
                    }
                },
                Order = new OrderDto()
                {
                    Currency = configuration.GenerateCardToken.Order.DefaultCurrency,
                    Amount = configuration.GenerateCardToken.Order.DefaultAmount,
                    Description = configuration.GenerateCardToken.Order.DefaultDescription,
                    TrackingId = paymentMethodId.ToString(),
                    AdditionalData = new AdditionalDataDto()    
                    {
                        Contract = configuration.GenerateCardToken.Order.AdditionalData?.Contract
                    },
                },
            }
        };
    }
}