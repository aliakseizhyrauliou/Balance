using System.Data;
using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Enums;
using Barion.Balance.Domain.Events.PaymentSystemWidgetGenerations;
using Barion.Balance.Domain.Services;
using MediatR;

namespace Barion.Balance.Application.PaymentSystemWidgets.Commands;

/// <summary>
/// Создание виджета
/// </summary>
public record CreateWidgetCommand : IRequest
{
    public required WidgetReason WidgetReason { get; set; }
    public required string OperatorId { get; set; }
    public string? PaidResourceId { get; set; }
    public string? AdditionalData { get; set; }

    public required int PaidResourceTypeId { get; set; }

    public decimal Amount { get; set; }
}

public sealed class CreateWidgetCommandHandler(
    IPaymentSystemService paymentSystemService,
    IPaymentSystemConfigurationRepository paymentSystemConfigurationRepository,
    IPaymentSystemWidgetGenerationRepository paymentSystemWidgetGenerationRepository,
    IUser currentUserData)
    : IRequestHandler<CreateWidgetCommand>
{
    private const string DefaultPaidResourceIdNameInCaseCreatePaymentMethod = "CREATE PAYMENT METHOD";
    private const string DefaultPaidResourceIdNameInCasePayment = "PAYMENT";
    
    public async Task Handle(CreateWidgetCommand request,
        CancellationToken cancellationToken)
    {
        var currentPaymentSchemaConfiguration =
            await paymentSystemConfigurationRepository.GetCurrentSchemaAsync(cancellationToken);

        if (currentPaymentSchemaConfiguration is null)
        {
            throw new Exception("current_payment_system_configuration_not_found");
        }

        
        //Создам модель, которая отражет причину открытия виджета платежной системы

        var paymentSystemWidgetGeneration = BuildPaymentSystemWidgetGeneration(currentPaymentSchemaConfiguration.Id, request);


        paymentSystemWidgetGeneration.AddDomainEvent(new PaymentSystemWidgetGenerationCreatedEvent(paymentSystemWidgetGeneration));

        await using var transaction = await paymentSystemWidgetGenerationRepository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);

        try
        {
            await paymentSystemWidgetGenerationRepository.DisableAllUserWidgetsAsync(paymentSystemWidgetGeneration.UserId, cancellationToken);
            await paymentSystemWidgetGenerationRepository.InsertAsync(paymentSystemWidgetGeneration, cancellationToken);

            var url = await paymentSystemService.GeneratePaymentSystemWidget(paymentSystemWidgetGeneration,
                currentPaymentSchemaConfiguration,
                cancellationToken);

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

    private Domain.Entities.PaymentSystemWidget BuildPaymentSystemWidgetGeneration(int currentPaymentSystemConfiguration,
        CreateWidgetCommand request)  
    {
        return new Domain.Entities.PaymentSystemWidget
        {
            UserId = currentUserData.Id!,
            FirstName = currentUserData.FirstName!,
            LastName = currentUserData.LastName!,
            WidgetReason = request.WidgetReason,
            PaymentSystemConfigurationId = currentPaymentSystemConfiguration,
            PaidResourceId = request.PaidResourceId ?? DefaultPaidResourceIdNameInCaseCreatePaymentMethod,
            OperatorId = request.OperatorId,
            AdditionalData = request.AdditionalData,
            PaidResourceTypeId = request.PaidResourceTypeId,
            Amount = request.Amount
        };
    }
}