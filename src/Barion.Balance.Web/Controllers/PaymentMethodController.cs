using Barion.Balance.Application.PaymentMethods.Commands;
using Barion.Balance.Application.PaymentMethods.Queries;
using Barion.Balance.Application.PaymentSystemWidgets.Queries;
using Barion.Balance.UseCases.PaymentSystemWidgets.Dtos;
using Barion.Balance.UseCases.PaymentSystemWidgets.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

/// <summary>
/// Интерфейс для работы с картами
/// </summary>
public class PaymentMethodController(ISender sender, 
    IMediator mediator,
    IWidgetUseCases widgetUseCases) : MediatrController(sender, mediator)
{
    /// <summary>
    /// Создать ссылку для привязки карты
    /// </summary>
    /// <param name="operatorId"></param>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("generateCreatePaymentMethodWidget")]
    public async Task<CheckoutDto> GenerateCreatePaymentMethodWidget([FromBody] GeneratePaymentMethodWidgetDto dto,
        CancellationToken cancellationToken)
    {
        return await widgetUseCases.GenerateWidgetForCreatePaymentMethodAsync(dto, cancellationToken);
    }
    
    /// <summary>
    /// Получить карту по id
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getById")]
    public async Task<PaymentMethodDto> GetById([FromQuery]GetPaymentMethodByIdQuery query, 
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Выбрать карту в качестве стандартного средства платежа
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost("selectByDefault")]
    public async Task SelectPaymentMethod([FromBody] SelectPaymentMethodCommand query,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(query, cancellationToken);
    }

    
    /// <summary>
    /// Удалить платежную карту
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    [HttpDelete("deletePaymentMethod")]
    public async Task DeletePaymentMethod([FromQuery] DeletePaymentMethodCommand query,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить коллекцию платежных методов
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<PaymentMethodDto>> List(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetPaymentMethodsListQuery(), cancellationToken);
    }
}

