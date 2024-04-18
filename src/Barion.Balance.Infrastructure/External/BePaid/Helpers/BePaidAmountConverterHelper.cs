namespace Barion.Balance.Infrastructure.External.BePaid.Helpers;

public static class BePaidAmountConverterHelper
{
    public static int ConvertToBePaidFormat(decimal amount)
    {
        return (int)(amount * 100);
    }
}