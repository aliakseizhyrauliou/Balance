using Barion.Balance.Application.Debtors.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

public class DebtorController(ISender sender, IMediator mediator) : MediatrController(sender, mediator)
{
    [HttpPost("captureDebtor")]
    public async Task CaptureDebtor([FromBody] CaptureDebtorCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
    }
}