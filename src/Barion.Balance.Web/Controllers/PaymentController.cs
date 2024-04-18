using Barion.Balance.Application.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

public class PaymentController(ISender sender, IMediator mediator) : MediatrController(sender, mediator)
{
    [HttpPost("payment")]
    public async Task Payment(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
    }
}