using Barion.Balance.Application.PaymentSystemWidgets.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class PaymentSystemWidgetController(ISender sender, IMediator mediator)
    : MediatrController(sender, mediator)
{
    /// <summary>
    /// Получить активный виджет пользователя
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getActive")]
    public async Task<CheckoutDto> GetActiveWidget(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetActivePaymentSystemWidgetQuery(), cancellationToken);
    }
}