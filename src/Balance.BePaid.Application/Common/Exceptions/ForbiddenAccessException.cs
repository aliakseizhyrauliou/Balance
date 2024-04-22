namespace Balance.BePaid.Application.Common.Exceptions;

public class ForbiddenAccessException(string message) : Exception(message);