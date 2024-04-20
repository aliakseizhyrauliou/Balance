using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.PaymentSystemWidgets.Commands;
using Barion.Balance.Application.PaymentSystemWidgets.Queries;
using Barion.Balance.Domain.Enums;
using Barion.Balance.UseCases.Base;
using Barion.Balance.UseCases.PaymentSystemWidgets.Dtos;
using Barion.Balance.UseCases.PaymentSystemWidgets.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace Barion.Balance.UseCases.PaymentSystemWidgets.Implementations;

public class WidgetUseCases(IMediator mediator, IUser currentUser) 
    : BaseUseCases(mediator, currentUser), IWidgetUseCases
{
    public async Task<CheckoutDto> GenerateWidgetForCreatePaymentMethodAsync(GeneratePaymentMethodWidgetDto dto,
        CancellationToken cancellationToken = default)
    {
        await mediator.Send(new CreateWidgetCommand
        {
            WidgetReason = WidgetReason.CreatePaymentMethod,
            OperatorId = dto.OperatorId,
            AdditionalData = JsonConvert.SerializeObject(dto.AdditionalData),
            PaidResourceId = dto.PaidResourceId,
            PaidResourceTypeId = dto.PaidResourceTypeId,
            PaymentSystemConfigurationId = dto.PaymentSystemConfigurationId
        }, cancellationToken);

        var checkout = await mediator.Send(new GetActivePaymentSystemWidgetQuery(), cancellationToken);

        return checkout;
    }

    public async Task<CheckoutDto> GenerateWidgetForPayment(GeneratePaymentWidgetDto dto,
        CancellationToken cancellationToken = default)
    {
        await mediator.Send(new CreateWidgetCommand
        {
            WidgetReason = WidgetReason.Payment,
            OperatorId = dto.OperatorId,
            AdditionalData = JsonConvert.SerializeObject(dto.AdditionalData),
            PaidResourceId = dto.PaidResourceId,
            PaidResourceTypeId = dto.PaidResourceTypeId,
            Amount = dto.Amount,
            PaymentSystemConfigurationId = dto.PaymentSystemConfigurationId
        }, cancellationToken);

        var checkout = await mediator.Send(new GetActivePaymentSystemWidgetQuery(), cancellationToken);

        return checkout;
    }
}