using Barion.Balance.Application.Holds.Commands;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services;
using Barion.Balance.Infrastructure.External.BePaid;
using Barion.Balance.Infrastructure.External.BePaid.Configuration;
using Barion.Balance.Infrastructure.External.BePaid.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

/// <summary>
/// Контроллер для работы с холдами
/// </summary>
/// <param name="sender"></param>
/// <param name="mediator"></param>
public class HoldController(ISender sender, IMediator mediator, IPaymentSystemConfigurationService configurationService) : MediatrController(sender, mediator)
{
    [HttpPost("hold")]
    public async Task Hold([FromBody] MakeHoldCommand command, CancellationToken cancellationToken)
    { 
        await _mediator.Send(command, cancellationToken);
    }
}