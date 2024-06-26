using Balance.BePaid.Application.Payments.Commands;
using Balance.BePaid.Application.PaymentSystemWidgets.Queries;
using Balance.BePaid.UseCases.Common;
using Balance.BePaid.UseCases.Payments.Dtos;
using Balance.BePaid.UseCases.Payments.Interfaces;
using Balance.BePaid.UseCases.PaymentSystemWidgets.Dtos;
using Balance.BePaid.UseCases.PaymentSystemWidgets.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Balance.BePaid.Web.Controllers;

/// <summary>
/// Платежи
/// </summary>
/// <param name="sender"></param>
/// <param name="mediator"></param>
/// <param name="widgetUseCases"></param>
public class PaymentController(ISender sender,
    IMediator mediator,
    IWidgetUseCases widgetUseCases,
    IPaymentUseCases paymentUseCases) 
    : MediatrController(sender, mediator)
{

    /// <summary>
    /// Генерация виджета для платежа
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("generatePaymentWidget")]
    public async Task<CheckoutDto> GeneratePaymentWidget([FromBody] GeneratePaymentWidgetDto dto,
        CancellationToken cancellationToken)
    {
        return await widgetUseCases.GenerateWidgetForPayment(dto, cancellationToken);
    }

    /// <summary>
    /// Совершить платеж
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost("payment")]
    public async Task Payment([FromBody] CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Совершить платеж выбраным пользователем платежным методом
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost("paymentWithSelectedPaymentMethod")]
    [ProducesResponseType(typeof(CreatedEntityDto<int>), 200)]
    public async Task<PaymentResponseDto> PaymentWithSelectedPaymentMethod([FromBody] PaymentWithSelectedPaymentMethodDto dto,
        CancellationToken cancellationToken = default)
    {
        return await paymentUseCases.PaymentWithSelectedPaymentMethod(dto, cancellationToken);
    }

    /// <summary>
    /// Возврат средств
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost("refund")]
    public async Task Refund([FromBody] RefundPaymentCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
    }
}