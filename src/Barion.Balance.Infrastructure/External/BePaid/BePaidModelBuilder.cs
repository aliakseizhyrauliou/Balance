using Barion.Balance.Domain.Entities;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Customer;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Order;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Settings;
using PaymentMethod = Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.PaymentMethod.PaymentMethod;

namespace Barion.Balance.Infrastructure.External.BePaid;

public static class BePaidModelBuilder
{
    public static CheckoutRoot BuildForAuthorizationWithWidget(
        BePaidConfiguration configuration,
        PaymentSystemWidgetGeneration paymentSystemWidgetGeneration)
    {
        return new CheckoutRoot()
        {
            Checkout = new Checkout
            {
                Test = configuration.GenerateCardToken.IsTest,
                TransactionType = configuration.GenerateCardToken.TransactionType,
                Attempts = configuration.GenerateCardToken.AttemptsCount,
                Settings = new Settings
                {
                    ButtonText = configuration.GenerateCardToken.Settings.DefaultButtonText,
                    Language = configuration.GenerateCardToken.Settings.DefaultLanguage,
                    CustomerFields = new CustomerFields()
                    {
                        Visible = ["email", "first_name", "last_name"],
                        ReadOnly = ["first_name", "last_name"]
                    },
                    SaveCardToggle = new SaveCardToggle()
                    {
                        Display = true,
                        CustomerContract = true
                    },
                    NotificationUrl = "Notif url"
                },
                Order = new Order()
                {
                    Currency = configuration.GenerateCardToken.Order.DefaultCurrency,
                    Amount = configuration.GenerateCardToken.Order.DefaultAmount,
                    Description = configuration.GenerateCardToken.Order.DefaultDescription,
                    TrackingId = paymentSystemWidgetGeneration.TrackingId.ToString(),
                    AdditionalData = new AdditionalData()
                    {
                        ReceiptText = ["asd"],
                        Contract = configuration.GenerateCardToken.Order.AdditionalData?.Contract
                    },
                },
                PaymentMethod = new PaymentMethod
                {
                    ExcludedTypes = ["erip", "halva"]
                },
                Customer = new Customer
                {
                    FirstName = "Alex",
                    LastName = "Zhurauliou"
                },
            }
        };
    }
}