using Barion.Balance.Application.Common.Interfaces;

namespace Barion.Balance.Web.Services;

public class CurrentUserMock : IUser
{
    public string? Id => "mock_id";
    public string? FirstName => "Alexey";
    public string? LastName => "Zhurauliou";
}