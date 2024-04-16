using Barion.Balance.Application.PaidResourseTypes.Commands;
using Barion.Balance.Application.PaidResourseTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

/// <summary>
/// Контроллер для работы с типами платных ресурсов
/// </summary>
/// <param name="sender"></param>
/// <param name="mediator"></param>
public class PaidResourceTypeController(ISender sender, IMediator mediator) : MediatrController(sender, mediator)
{
    [HttpPost("create")]
    public async Task Create(CreatePaidResourceTypeCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
    }

    [HttpGet("list")]
    public async Task<List<PaidResourceTypeDto>> List(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetPaidResourceTypesQuery(), cancellationToken);
    }

}