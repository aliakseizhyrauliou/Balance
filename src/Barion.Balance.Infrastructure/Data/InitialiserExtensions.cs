using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Customer;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Order;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Settings;
using Barion.Balance.Infrastructure.External.BePaid.Configuration;
using Barion.Balance.Infrastructure.External.BePaid.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentMethod = Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.PaymentMethod.PaymentMethod;

namespace Barion.Balance.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initializer.InitialiseAsync();

        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
    IBalanceDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.MigrateDatabase();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync(IBalanceDbContext context)
    {
        SeedBePaidConfiguration(context);
    }

    private void SeedBePaidConfiguration(IBalanceDbContext balanceDbContext)
    {
        if (balanceDbContext.PaymentSystemConfigurations.Any(x => x.PaymentSystemName == "BePaid"))
        {
            return;
        }

        var bePaidConfiguration = new BePaidConfiguration()
        {
            Urls = new Urls
            {
                CheckoutUrl = new Urls.UrlWithDescription()
                {
                    Url = "https://checkout.bepaid.by/ctp/api/checkouts",
                    Description = "Адрес для создания платежного виджета"
                },
                NotificationUrl = new Urls.UrlWithDescription()
                {
                    Url = "https://charger.barion-ev.by/api/BePaid/VerifyPaymentMethodNotification",
                    Description = "Веб-хук адрес, куда будет делать запрос платежная система"
                },
                RefundUrl = new Urls.UrlWithDescription()
                {
                    Url = "https://gateway.bepaid.by/transactions/refunds",
                    Description = "Адрес для возврата средств"
                },
                AuthorizationUrl = new Urls.UrlWithDescription()
                {
                    Url = "https://gateway.bepaid.by/transactions/authorizations",
                    Description = "Адрес для созания холда"
                },
                PaymentUrl = new Urls.UrlWithDescription()
                {
                    Url = "https://gateway.bepaid.by/transactions/payments",
                    Description = "Адрес для создания платежа"
                },
                CaptureHoldUrl = new Urls.UrlWithDescription()
                {
                    Url = "https://gateway.bepaid.by/transactions/captures",
                    Description = "Адрес для фиксирования холда"
                },
                VoidHold = new Urls.UrlWithDescription()
                {
                    Url = "https://gateway.bepaid.by/transactions/voids",
                    Description = "Адрес для отмены холда"
                }
            },
            CheckoutAuthorization = new Checkout
            {
                Attempts = 1,
                TransactionType = TransactionTypes.Authorization,
                Settings = new Settings
                {
                    ButtonText = "Привязать карту",
                    Language = "RU",
                    NotificationUrl = "https://7761-134-17-4-170.ngrok-free.app/api/BePaid/verifyPaymentMethodNotification",
                    SaveCardToggle = new SaveCardToggle
                    {
                        Display = true,
                        CustomerContract = true
                    },
                    CustomerFields = new CustomerFields
                    {
                        Visible = ["email", "first_name", "last_name"],
                        ReadOnly = ["first_name", "last_name"]
                    }
                },
                
                
                Order = new Order
                {
                    Currency = "BYN",
                    Amount = 0,
                    Description = "Продолжите для привязки карты к системe Barion",
                    AdditionalData = new AdditionalData
                    {
                        Contract = ["recurring", "card_on_file"],
                        ReceiptText = ["Карта успешно привязана к системе Barion"]
                    },
                    
                    TrackingId = null
                },
                
                Test = true,
                PaymentMethod = new PaymentMethod
                {
                    ExcludedTypes = ["erip", "halva"]
                },
                Customer = new Customer
                {
                    FirstName = "Default FirstName",
                    LastName = "Default LastName"
                }
            }
        };

        var configurationModel = new PaymentSystemConfiguration()
        {
            PaymentSystemName = "BePaid",
            IsCurrentSchema = true,
            Data = JsonConvert.SerializeObject(bePaidConfiguration)
        };


        balanceDbContext.PaymentSystemConfigurations.Add(configurationModel);
        balanceDbContext.SaveChanges();
    }
}