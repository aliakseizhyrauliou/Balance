using System.ComponentModel.DataAnnotations.Schema;
using Barion.Balance.Application.Common.Exceptions;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Exceptions;
using Barion.Balance.Domain.Services;
using MediatR;

namespace Barion.Balance.Application.Payments;

public class CreatePaymentCommand : IRequest
{
    /// <summary>
    /// Идентификатор пользователя 
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Сумма платежа
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    ///Id того, за что была оплата
    ///Id бронирования, зарядки или парковки
    /// </summary>
    public required string PaidResourceId { get; set; }

    /// <summary>
    /// Специфическая инфа платежа.
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string? AdditionalData { get; set; }


    /// <summary>
    /// Получатель суммы транзакции
    /// </summary>
    public required string OperatorId { get; set; }


    /// <summary>
    /// Была ли оплата за счет бонусов
    /// </summary>
    public bool IsBonus { get; set; }

    /// <summary>
    /// Id платежного метода
    /// </summary>
    public required int PaymentMethodId { get; set; }

    public required int PaidResourceTypeId { get; set; }
}

public class CreatePaymentCommandHandler(IPaymentSystemService paymentSystemService,
    IPaidResourceTypeRepository paidResourceTypeRepository,
    IPaymentMethodRepository paymentMethodRepository,
    IAccountRecordRepository accountRecordRepository) : IRequestHandler<CreatePaymentCommand>
{
    public async Task Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = await paymentMethodRepository.GetByIdAsync(request.PaymentMethodId, cancellationToken);

        if (paymentMethod is null)
        {
            throw new NotFoundException("payment_method_not_found");
        }

        if (!await paidResourceTypeRepository.AnyAsync(x => x.Id == request.PaidResourceTypeId, cancellationToken))
        {
            throw new NotFoundException("paid_resource_not_found");
        }

        var accountRecord = new Payment
        {
            UserId = request.UserId,
            Amount = request.Amount,
            PaidResourceId = request.PaidResourceId,
            PaymentSystemFinancialTransactionId = null,
            OperatorId = request.OperatorId,
            PaymentMethodId = request.PaymentMethodId,
            PaidResourceTypeId = request.PaymentMethodId,
            AdditionalData = request.AdditionalData,
        };

        var paymentResult = await paymentSystemService.Payment(accountRecord, paymentMethod, cancellationToken);

        if (paymentResult is not {IsOk: true, AccountRecord: not null})
            throw new PaymentSystemException(paymentResult.FriendlyErrorMessage);

        await accountRecordRepository.InsertAsync(paymentResult.AccountRecord!, cancellationToken);
    }
}