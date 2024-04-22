using Balance.BePaid.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Balance.BePaid.Web.Infrastructure.Attributes;

public class EnsureBePaidAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            var authorizationService = context.HttpContext.RequestServices.GetService(typeof(IPaymentSystemAuthorizationService)) as IPaymentSystemAuthorizationService;

            var authorizationHeaderValues = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeaderValues) || authorizationHeaderValues.Count < 1)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var authParts = authorizationHeaderValues[0]!.Split(' ');

            if (authParts.Length != 2 || !authParts[0].Equals("Basic", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!authorizationService!.ValidateReceivedWebHookRequest(authParts[1]))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
        catch (Exception ex)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
