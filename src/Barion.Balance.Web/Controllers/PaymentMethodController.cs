using Barion.Balance.Application.PaymentMethods.Commands;
using Barion.Balance.Application.PaymentMethods.Queries;
using Barion.Balance.Application.PaymentSystemWidgetGenerations.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

/// <summary>
/// Интерфейс для работы с картами
/// </summary>
public class PaymentMethodController(ISender sender, IMediator mediator) 
    : MediatrController(sender, mediator)
{
    [HttpPost("create")]
    public async Task Create(CancellationToken cancellationToken)
    {
        await _sender.Send(new CreatePaymentSystemWidgetGenerationCommand(), cancellationToken);
    }
    
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
    [HttpDelete]
    public async Task DeletePaymentMethod([FromQuery] DeletePaymentMethodCommand query,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(query, cancellationToken);
    }

    [HttpGet("list")]
    public async Task<List<PaymentMethodDto>> List(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetPaymentMethodsListQuery(), cancellationToken);
    }
}

