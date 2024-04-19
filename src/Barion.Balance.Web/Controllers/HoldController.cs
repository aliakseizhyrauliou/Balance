using Barion.Balance.Application.Holds.Commands;
using Barion.Balance.Application.Holds.Queries;
using Barion.Balance.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

/// <summary>
/// Контроллер для работы с холдами
/// </summary>
/// <param name="sender"></param>
/// <param name="mediator"></param>
public class HoldController(ISender sender, IMediator mediator, IPaymentSystemConfigurationService configurationService) 
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