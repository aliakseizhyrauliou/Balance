using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Balance.BePaid.Web.Infrastructure.Attributes;

public class SetBePaidIsCurrentUserAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        const string externalSystemName = "BePaidWebHook";
        
        var claim = new Claim(ClaimTypes.NameIdentifier, externalSystemName);

        var claimsIdentity = new ClaimsIdentity(new[] { claim });
        context.HttpContext.User.AddIdentity(claimsIdentity);

        base.OnActionExecuting(context);
    }
}
