using Barion.Balance.Application.Receipts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

/// <summary>
/// Работа с чеками
/// </summary>
/// <param name="sender"></param>
/// <param name="mediator"></param>
public class ReceiptController(ISender sender, IMediator mediator) : MediatrController(sender, mediator)
{
    /// <summary>
    /// Получить коллекцию чеков
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<List<ReceiptDto>> List(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetReceiptListQuery(), cancellationToken);
    }
}