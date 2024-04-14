using System.Security.Claims;
using Barion.Balance.Application.Common.Interfaces;

namespace Barion.Balance.Web.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{
    public string? Id => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? FirstName => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.GivenName);
    public string? LastName => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Surname);
}