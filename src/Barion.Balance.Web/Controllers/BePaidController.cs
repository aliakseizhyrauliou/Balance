using Barion.Balance.Application.PaymentSystemWidgetGenerations.Commands;
using Barion.Balance.Web.Infrastructure.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

public class BePaidController(ISender sender, IMediator mediator)
    : MediatrController(sender, mediator)
{
    [HttpPost("verifyPaymentMethodNotification")]
    public async Task<IActionResult> VerifyPaymentMethodNotification(CancellationToken cancellationToken)
    {
        var model = await BodyReaderHelper.ReadBody(HttpContext.Request);

        await sender.Send(new ProcessWidgetResponseCommand { JsonResponse = model }, cancellationToken);
        
        return Ok();
    }
}   