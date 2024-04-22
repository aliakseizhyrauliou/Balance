using Balance.BePaid.Application.Common.Interfaces;

namespace Balance.BePaid.Web.Services;

public class CurrentUserMock : IUser
{
    public string? Id => "mock_id";
    public string? FirstName => "Alexey";
    public string? LastName => "Zhurauliou";
}