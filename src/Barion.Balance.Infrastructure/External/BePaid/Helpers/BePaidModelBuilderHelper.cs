using Barion.Balance.Domain.Entities;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Customer;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Order;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Settings;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.CreateTransaction;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.ParentId;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Barion.Balance.Infrastructure.External.BePaid.Configuration;
using PaymentMethod = Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.PaymentMethod.PaymentMethod;

namespace Barion.Balance.Infrastructure.External.BePaid.Helpers;

public static class BePaidModelBuilderHelper
{

    public static CreateTransactionRoot BuildPaymentModel(int amount, string cardToken,
        BePaidConfiguration configuration)
    {
        return new CreateTransactionRoot
        {
            Request = new CreateTransaction()
            {
                Amount = amount,
                Currency = "BYN",
                Description = "Платеж",
                Language = "ru",
                Test = true,
                CreditCard = new CreateTransactionCreditCard()
                {
                    Token = cardToken
                }
            }
        };
    }
    public static ParentIdRoot BuildParentIdModel(string parentId, int amount)
    {
        return new ParentIdRoot()
        {
            Request = new ParentIdRequest()
            {
                ParentId = parentId,
                Amount = amount
            }
        };
    }

    public static CreateTransactionRoot BuildHoldModel(Hold hold,
        string cardToken,
        BePaidConfiguration configuration)
    {
        return new CreateTransactionRoot
        {
            Request = new CreateTransaction
            {
                Amount = (int)hold.Amount * 100,
                Currency = "BYN",
                Description = "HOLD",
                TrackingId = hold.Id.ToString(),
                DuplicateCheck = true,
                Language = "RU",
                Test = true,
                CreditCard = new CreateTransactionCreditCard
                {
                    Token = cardToken
                }
            }
        };
    }

    public static CheckoutRoot BuildForAuthorizationWithWidget(
        BePaidConfiguration configuration,
        PaymentSystemWidgetGeneration paymentSystemWidgetGeneration)
    {
        return new CheckoutRoot
        {
            Checkout = new Checkout
            {
                Test = configuration.CheckoutAuthorization.Test,
                TransactionType = configuration.CheckoutAuthorization.TransactionType,
                Attempts = configuration.CheckoutAuthorization.Attempts,
                Settings = new Settings
                {
                    ButtonText = configuration.CheckoutAuthorization.Settings.ButtonText,
                    Language = configuration.CheckoutAuthorization.Settings.Language,
                    CustomerFields = new CustomerFields
                    {
                        Visible = configuration.CheckoutAuthorization.Settings.CustomerFields.Visible,
                        ReadOnly = configuration.CheckoutAuthorization.Settings.CustomerFields.ReadOnly
                    },
                    SaveCardToggle = new SaveCardToggle
                    {
                        Display = configuration.CheckoutAuthorization.Settings.SaveCardToggle.Display,
                        CustomerContract = configuration.CheckoutAuthorization.Settings.SaveCardToggle.CustomerContract
                    },
                    NotificationUrl = configuration.CheckoutAuthorization.Settings.NotificationUrl
                },
                Order = new Order
                {
                    Currency = configuration.CheckoutAuthorization.Order.Currency,
                    Amount = configuration.CheckoutAuthorization.Order.Amount,
                    Description = configuration.CheckoutAuthorization.Order.Description,
                    TrackingId = paymentSystemWidgetGeneration.Id.ToString()!,
                    AdditionalData = new AdditionalData
                    {
                        ReceiptText = configuration.CheckoutAuthorization.Order.AdditionalData.ReceiptText,
                        Contract = configuration.CheckoutAuthorization.Order.AdditionalData.Contract,
                    },
                },
                PaymentMethod = new PaymentMethod
                {
                    ExcludedTypes = configuration.CheckoutAuthorization.PaymentMethod.ExcludedTypes
                },
                Customer = new Customer
                {
                    FirstName = paymentSystemWidgetGeneration.FirstName,
                    LastName = paymentSystemWidgetGeneration.LastName
                }
            }
        };
    }
}