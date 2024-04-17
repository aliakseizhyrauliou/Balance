using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Enums;
using Barion.Balance.Domain.Events.PaymentSystemWidgetGenerations;
using Barion.Balance.Domain.Services;
using MediatR;
using System.Data;

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

        var paymentSystemWidgetGeneration = BuildPaymentSystemWidgetGeneration(currentPaymentSchemaConfiguration.Id);


        paymentSystemWidgetGeneration.AddDomainEvent(new PaymentSystemWidgetGenerationCreatedEvent(paymentSystemWidgetGeneration));

        await using var transaction = await paymentSystemWidgetGenerationRepository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);

        try
        {
            await paymentSystemWidgetGenerationRepository.DisableAllUserWidgetsAsync(paymentSystemWidgetGeneration.UserId, cancellationToken);
            await paymentSystemWidgetGenerationRepository.InsertAsync(paymentSystemWidgetGeneration, cancellationToken);

            var url = await paymentSystemService.GeneratePaymentSystemWidget(paymentSystemWidgetGeneration, cancellationToken);

            paymentSystemWidgetGeneration.Url = url;
            paymentSystemWidgetGeneration.IsSuccess = true;

            await paymentSystemWidgetGenerationRepository.UpdateAsync(paymentSystemWidgetGeneration, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception) 
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private PaymentSystemWidgetGeneration BuildPaymentSystemWidgetGeneration(int currentPaymentSystemConfiguration)  
    {
        return new PaymentSystemWidgetGeneration
        {
            UserId = currentUserData.Id,
            FirstName = currentUserData.FirstName,
            LastName = currentUserData.LastName,
            WidgetReason = WidgetReason.CreatePaymentMethod,
            PaymentSystemConfigurationId = currentPaymentSystemConfiguration
        };
    }
}