using System.Security.Claims;
using Balance.BePaid.Application.Common.Interfaces;

namespace Balance.BePaid.Web.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{
    public string? Id => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? FirstName => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.GivenName);
    public string? LastName => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Surname);
}