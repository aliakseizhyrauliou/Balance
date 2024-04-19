using Barion.Balance.Application.Holds.Commands;
using Barion.Balance.Application.Holds.Queries;
using Barion.Balance.UseCases.Common;
using Barion.Balance.UseCases.Holds.Dtos;
using Barion.Balance.UseCases.Holds.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

/// <summary>
/// Контроллер для работы с холдами
/// </summary>
/// <param name="sender"></param>
/// <param name="mediator"></param>
public class HoldController(ISender sender, 
    IMediator mediator,
    IHoldUseCases holdUseCases) 
    : MediatrController(sender, mediator)
{
    
    /// <summary>
    /// Возвращает коллекцию холдов
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<HoldDto>> List(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetHoldListQuery(), cancellationToken);
    }

    /// <summary>
    /// Отменяет холд
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost("voidHold")]
    public async Task VoidHold([FromBody] VoidHoldCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Захолдировать сумму
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost("hold")]
    public async Task Hold([FromBody] MakeHoldCommand command, CancellationToken cancellationToken)
    { 
        await _mediator.Send(command, cancellationToken);
    }


    /// <summary>
    /// Захолдировать сумму с выбранного пользователем платежного метода
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("holdWithSelectedPaymentMethod")]
    [ProducesResponseType(typeof(CreatedEntityDto<int>), 200)]
    public async Task<CreatedEntityDto<int>> HoldWithSelectedPaymentMethod(
        [FromBody] HoldWithSelectedPaymentMethodDto dto,
        CancellationToken cancellationToken)
    {
        return await holdUseCases.HoldWithSelectedPaymentMethod(dto, cancellationToken);
    }

    /// <summary>
    /// Удержать сумму холда
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost("captureHold")]
    public async Task CaptureHold([FromBody] CaptureHoldCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }
}