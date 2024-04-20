namespace Barion.Balance.UseCases.Common;

public class PaymentResponseDto 
{
    public bool IsPaid { get; set; }
    public bool IsWrittenToDebtors { get; set; }
    public int? DebtorId { get; set; }
    public int? PaymentId { get; set; }
}