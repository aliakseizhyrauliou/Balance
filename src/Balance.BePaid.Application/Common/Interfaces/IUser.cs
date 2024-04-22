namespace Balance.BePaid.Application.Common.Interfaces;

public interface IUser
{
    string? Id { get; }
    string? FirstName { get;}
    string? LastName { get; }
}