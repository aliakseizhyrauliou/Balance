using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services;
using Barion.Balance.Domain.Services.Models;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels;

namespace Barion.Balance.Infrastructure.External.BePaid;

public class BePaidService(IHoldRepository holdRepository,
    BePaidConfigurationService configurationService,
    HttpClient httpClient) 
    : IPaymentSystemService
{
    private const string PaymentSystemName = "BePaid";
    public Task<Hold> MakeHold(MakeHold makeHold, 
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CaptureHold(Hold hold, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VoidHold(Hold hold, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    private async Task<CheckouRootDto> BuildCheckoutRootDto(MakeHold hold)
    {
        var configuration = await GetBePaidConfiguration();
        
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
                    NotificationUrl = configuration.GenerateCardToken.Settings.NotificationUrl,
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
                    TrackingId = hold.PaymentMethodId.ToString(),
                    AdditionalData = new AdditionalDataDto()    
                    {
                        Contract = configuration.GenerateCardToken.Order.AdditionalData.Contract
                    },
                },
            }
        };
    }

    private async Task<BePaidConfiguration> GetBePaidConfiguration()
    {
        var configurationModel = await configurationService.GetPaymentSystemConfiguration(PaymentSystemName);

        return BePaidConfigurationDeserializationHelper.DeserializeToBePaidConfiguration(configurationModel);
    }

}