using Barion.Balance.Application.PaymentMethods.Commands;
using Barion.Balance.Application.PaymentMethods.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

public class PaymentMethodController(ISender sender, IMediator mediator) 
    : MediatrController(sender, mediator)
{

    [HttpPost("create")]
    public async Task<PaymentMethodDto> Create([FromBody] CreatePaymentMethodCommand command,
        CancellationToken cancellationToken)
    {
        return await _sender.Send(command, cancellationToken);
    }

    [HttpGet("getById")]
    public async Task<PaymentMethodDto> GetById([FromQuery]GetPaymentMethodByIdQuery query, 
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost("selectByDefault")]
    public async Task SelectPaymentMethod([FromBody] SelectPaymentMethodCommand query,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(query, cancellationToken);
    }

    [HttpDelete]
    public async Task DeletePaymentMethod([FromQuery] DeletePaymentMethodCommand query,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(query, cancellationToken);
    }
}

