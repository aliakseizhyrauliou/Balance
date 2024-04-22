using Balance.BePaid.Application.Debtors.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Balance.BePaid.Web.Controllers;

public class DebtorController(ISender sender, IMediator mediator) : MediatrController(sender, mediator)
{
    /// <summary>
    /// Удержать сумму с должника
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost("captureDebtor")]
    public async Task CaptureDebtor([FromBody] CaptureDebtorCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
    }
}