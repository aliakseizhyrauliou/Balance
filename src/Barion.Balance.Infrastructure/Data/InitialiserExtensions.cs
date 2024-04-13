using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Infrastructure.External.BePaid;
using Barion.Balance.Infrastructure.External.BePaid.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            Urls = new Urls()
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
            GenerateCardToken = new GenerateCardToken()
            {
                IsTest = true,
                TransactionType = TransactionTypes.Authorization,
                AttemptsCount = 1,
                Settings = new GenerateCardToken.GenerateCardTokenSettings()
                {
                    DefaultButtonText = "Привязать карту",
                    DefaultLanguage = "ru",
                },
                Order = new GenerateCardToken.GenerateCardTokenOrder()
                {
                    DefaultCurrency = "BYN",
                    DefaultAmount = 0,
                    DefaultDescription = "Проверка карты",
                    AdditionalData = new GenerateCardToken.GenerateCardTokenOrder.GenerateCardTokenAdditionalData()
                    {
                        Contract = new List<string>() { "recurring" }
                    }
                }
            }
        };

        var configurationModel = new PaymentSystemConfiguration()
        {
            PaymentSystemName = "BePaid",
            Data = JsonConvert.SerializeObject(bePaidConfiguration)
        };


        balanceDbContext.PaymentSystemConfigurations.Add(configurationModel);
        balanceDbContext.SaveChanges();
    }
}