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
            Request = new CreateTransaction
            {
                Amount = amount,
                Currency = "BYN",
                Description = "Платеж",
                Language = "ru",
                Test = true,
                CreditCard = new CreateTransactionCreditCard
                {
                    Token = cardToken
                }
            }
        };
    }
    public static ParentIdRoot BuildParentIdModel(string parentId, int amount)
    {
        return new ParentIdRoot
        {
            Request = new ParentIdRequest
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
                Amount = BePaidAmountConverterHelper.ConvertToBePaidFormat(hold.Amount),
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

    public static CheckoutRoot BuildWidgetForCreatePaymentMethod(
        BePaidConfiguration configuration,
        PaymentSystemWidget paymentSystemWidget)
    {
        return new CheckoutRoot
        {
            Checkout = new Checkout
            {
                Test = configuration.CheckoutCreatePaymentMethod.Test,
                TransactionType = configuration.CheckoutCreatePaymentMethod.TransactionType,
                Attempts = configuration.CheckoutCreatePaymentMethod.Attempts,
                Settings = new Settings
                {
                    ButtonText = configuration.CheckoutCreatePaymentMethod.Settings.ButtonText,
                    Language = configuration.CheckoutCreatePaymentMethod.Settings.Language,
                    CustomerFields = new CustomerFields
                    {
                        Visible = configuration.CheckoutCreatePaymentMethod.Settings.CustomerFields.Visible,
                        ReadOnly = configuration.CheckoutCreatePaymentMethod.Settings.CustomerFields.ReadOnly
                    },
                    SaveCardToggle = new SaveCardToggle
                    {
                        Display = configuration.CheckoutCreatePaymentMethod.Settings.SaveCardToggle.Display,
                        CustomerContract = configuration.CheckoutCreatePaymentMethod.Settings.SaveCardToggle.CustomerContract
                    },
                    NotificationUrl = configuration.CheckoutCreatePaymentMethod.Settings.NotificationUrl
                },
                Order = new Order
                {
                    Currency = configuration.CheckoutCreatePaymentMethod.Order.Currency,
                    Amount = configuration.CheckoutCreatePaymentMethod.Order.Amount,
                    Description = configuration.CheckoutCreatePaymentMethod.Order.Description,
                    TrackingId = paymentSystemWidget.Id.ToString()!,
                    AdditionalData = new AdditionalData
                    {
                        ReceiptText = configuration.CheckoutCreatePaymentMethod.Order.AdditionalData.ReceiptText,
                        Contract = configuration.CheckoutCreatePaymentMethod.Order.AdditionalData.Contract
                    }
                },
                PaymentMethod = new PaymentMethod
                {
                    ExcludedTypes = configuration.CheckoutCreatePaymentMethod.PaymentMethod.ExcludedTypes
                },
                Customer = new Customer
                {
                    FirstName = paymentSystemWidget.FirstName,
                    LastName = paymentSystemWidget.LastName
                }
            }
        };
    }
    
        public static CheckoutRoot BuildWidgetForPayment(
        BePaidConfiguration configuration,
        PaymentSystemWidget paymentSystemWidget,
        int amount)
    {
        return new CheckoutRoot
        {
            Checkout = new Checkout
            {
                Test = configuration.CheckoutPayment.Test,
                TransactionType = configuration.CheckoutPayment.TransactionType,
                Attempts = configuration.CheckoutPayment.Attempts,
                Settings = new Settings
                {
                    ButtonText = configuration.CheckoutPayment.Settings.ButtonText,
                    Language = configuration.CheckoutPayment.Settings.Language,
                    CustomerFields = new CustomerFields
                    {
                        Visible = configuration.CheckoutPayment.Settings.CustomerFields.Visible,
                        ReadOnly = configuration.CheckoutPayment.Settings.CustomerFields.ReadOnly
                    },
                    SaveCardToggle = new SaveCardToggle
                    {
                        Display = configuration.CheckoutPayment.Settings.SaveCardToggle.Display,
                        CustomerContract = configuration.CheckoutPayment.Settings.SaveCardToggle.CustomerContract
                    },
                    NotificationUrl = configuration.CheckoutPayment.Settings.NotificationUrl
                },
                Order = new Order
                {
                    Currency = configuration.CheckoutPayment.Order.Currency,
                    Amount = amount,
                    Description = configuration.CheckoutPayment.Order.Description,
                    TrackingId = paymentSystemWidget.Id.ToString()!,
                    AdditionalData = new AdditionalData
                    {
                        ReceiptText = configuration.CheckoutPayment.Order.AdditionalData.ReceiptText,
                        Contract = configuration.CheckoutPayment.Order.AdditionalData.Contract
                    }
                },
                PaymentMethod = new PaymentMethod
                {
                    ExcludedTypes = configuration.CheckoutPayment.PaymentMethod.ExcludedTypes
                },
                Customer = new Customer
                {
                    FirstName = paymentSystemWidget.FirstName,
                    LastName = paymentSystemWidget.LastName
                }
            }
        };
    }
}