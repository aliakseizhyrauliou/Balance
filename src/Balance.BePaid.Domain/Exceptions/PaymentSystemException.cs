namespace Balance.BePaid.Domain.Exceptions;

public class PaymentSystemException(string message) : Exception(message);