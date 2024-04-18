using System.ComponentModel.DataAnnotations.Schema;
using Barion.Balance.Domain.Common;

namespace Barion.Balance.Domain.Entities;

public class PaymentSystemConfiguration : BaseAuditableEntity
{
    /// <summary>
    /// Название платежной системы
    /// </summary>
    public required string PaymentSystemName { get; set; } 
    
    
    /// <summary>
    /// Дополнительная инфа, которая необходима для использования платежной системы
    /// </summary>
    [Column(TypeName = "jsonb")]
    public required string Data { get; set; }

    /// <summary>
    /// Информация об открытии виджета в платежной системе
    /// </summary>
    public ICollection<PaymentSystemWidgetGeneration>? PaymentSystemWidgetGenerations { get; set; }

    /// <summary>
    /// Холды платежной системы
    /// </summary>
    public ICollection<Hold>? Holds { get; set; }


    /// <summary>
    /// Платежи платежной системы
    /// </summary>
    public ICollection<Payment> Payments { get; set; }

    /// <summary>
    /// Чеки платежной системы
    /// </summary>
    public ICollection<Receipt> Receipts { get; set; }

    
    /// <summary>
    /// Является ли текущей платежной системой
    /// </summary>
    public bool IsCurrentSchema { get; set; }
}