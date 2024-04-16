using Barion.Balance.Application.PaymentSystemWidgetGenerations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

public class PaymentSystemWidgetController(ISender sender, IMediator mediator)
    : MediatrController(sender, mediator)
{
    [HttpGet("getActive")]
    public async Task<CheckoutDto> GetActiveWidget(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetActivePaymentSystemWidgetQuery(), cancellationToken);
    }
}