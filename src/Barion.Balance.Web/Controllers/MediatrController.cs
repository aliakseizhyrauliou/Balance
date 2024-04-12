using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barion.Balance.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediatrController(ISender sender, IMediator mediator) : ControllerBase
{
    protected IMediator _mediator = mediator;
    protected ISender _sender = sender;
}