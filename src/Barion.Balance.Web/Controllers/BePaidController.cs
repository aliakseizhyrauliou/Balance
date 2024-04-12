using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

public class BePaidController(ISender sender, IMediator mediator) 
    : MediatrController(sender, mediator)
{
    [HttpPost("verifyPaymentMethodNotification")]
    public async Task VerifyPaymentMethodNotification()
    {
        return;
    }
}