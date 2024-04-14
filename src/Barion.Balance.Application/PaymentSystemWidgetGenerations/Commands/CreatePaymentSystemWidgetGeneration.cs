using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Enums;
using Barion.Balance.Domain.Events.PaymentSystemWidgetGenerations;
using Barion.Balance.Domain.Services;
using MediatR;

namespace Barion.Balance.Application.PaymentSystemWidgetGenerations.Commands;

public record CreatePaymentSystemWidgetGenerationCommand : IRequest;

public sealed class CreatePaymentSystemWidgetGenerationCommandHandler(
    IPaymentSystemService paymentSystemService,
    IPaymentSystemConfigurationRepository paymentSystemConfigurationRepository,
    IPaymentSystemWidgetGenerationRepository paymentSystemWidgetGenerationRepository,
    IUser currentUserData)
    : IRequestHandler<CreatePaymentSystemWidgetGenerationCommand>
{
    public async Task Handle(CreatePaymentSystemWidgetGenerationCommand request,
        CancellationToken cancellationToken)
    {
        var currentPaymentSchemaConfiguration =
            await paymentSystemConfigurationRepository.GetCurrentSchemaAsync(cancellationToken);

        if (currentPaymentSchemaConfiguration is null)
        {
            throw new Exception("current_schema_not_specified");
        }

        //Создам модель, которая отражет причину открытия виджета платежной системы

        var paymentSystemWidgetGeneration = new PaymentSystemWidgetGeneration
        {
            UserId = currentUserData.Id,
            FirstName = currentUserData.FirstName,
            LastName = currentUserData.LastName,
            WidgetReason = WidgetReason.CreatePaymentMethod,
            PaymentSystemConfigurationId = currentPaymentSchemaConfiguration.Id,
        };
        
        paymentSystemWidgetGeneration.AddDomainEvent(new PaymentSystemWidgetGenerationCreatedEvent(paymentSystemWidgetGeneration));
        
        await paymentSystemWidgetGenerationRepository.InsertAsync(paymentSystemWidgetGeneration,
            cancellationToken);

        
        //TODO
        paymentSystemWidgetGeneration.TrackingId = paymentSystemWidgetGeneration.Id;

        try
        {
            var url = await paymentSystemService.GeneratePaymentSystemWidget(paymentSystemWidgetGeneration,
                cancellationToken);

            paymentSystemWidgetGeneration.Url = url;
            paymentSystemWidgetGeneration.IsSuccess = true;
        }
        catch (Exception ex)
        {
            paymentSystemWidgetGeneration.IsSuccess = false;
        }
        finally
        {
            await paymentSystemWidgetGenerationRepository.UpdateAsync(paymentSystemWidgetGeneration, cancellationToken);
        }
    }
}