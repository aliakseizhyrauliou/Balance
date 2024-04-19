using System.Data;
using Barion.Balance.Application.Common.Exceptions;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Events.Payments;
using Barion.Balance.Domain.Exceptions;
using Barion.Balance.Domain.Services;
using MediatR;
using Newtonsoft.Json;

namespace Barion.Balance.Application.Payments.Commands;

public class CreatePaymentCommand : IRequest<int>
{
    /// <summary>
    /// Идентификатор пользователя 
    /// </summary>
    public  string UserId { get; set; }

    /// <summary>
    /// Сумма платежа
    /// </summary>
    public  decimal Amount { get; set; }

    /// <summary>
    ///Id того, за что была оплата
    ///Id бронирования, зарядки или парковки
    /// </summary>
    public  string PaidResourceId { get; set; }

    /// <summary>
    /// Специфическая инфа платежа.
    /// </summary>
    public Dictionary<string, string>? AdditionalData { get; set; }
    

    /// <summary>
    /// Получатель суммы транзакции
    /// </summary>
    public  string OperatorId { get; set; }


    /// <summary>
    /// Была ли оплата за счет бонусов
    /// </summary>
    public bool IsBonus { get; set; }

    /// <summary>
    /// Id платежного метода
    /// </summary>
    public  int PaymentMethodId { get; set; }

    public  int PaidResourceTypeId { get; set; }
}

public class CreatePaymentCommandHandler(IPaymentSystemService paymentSystemService,
    IPaidResourceTypeRepository paidResourceTypeRepository,
    IPaymentMethodRepository paymentMethodRepository,
    IPaymentSystemConfigurationRepository paymentSystemConfigurationRepository,
    IPaymentRepository paymentsRepository) : IRequestHandler<CreatePaymentCommand, int>
{
    public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
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

        var paymentSystemcConfiguration = await paymentSystemConfigurationRepository.GetCurrentSchemaAsync(cancellationToken);

        if (paymentSystemcConfiguration is null)
        {
            throw new Exception("current_payment_system_configuration_not_found");
        }

        var accountRecord = new Payment
        {
            UserId = request.UserId,
            Amount = request.Amount,
            PaidResourceId = request.PaidResourceId,
            PaymentSystemTransactionId = null,
            OperatorId = request.OperatorId,
            PaymentMethodId = request.PaymentMethodId,
            PaidResourceTypeId = request.PaidResourceTypeId,
            AdditionalData = JsonConvert.SerializeObject(request.AdditionalData),
            PaymentSystemConfigurationId = paymentSystemcConfiguration.Id
        };

        var paymentResult = await paymentSystemService.Payment(accountRecord, 
            paymentMethod, 
            paymentSystemcConfiguration, 
            cancellationToken);
        
        if (paymentResult is not {IsOk: true, Payment: not null})
            throw new PaymentSystemException(paymentResult.FriendlyErrorMessage);

        await using var transaction = await paymentsRepository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);

        try
        {
            paymentResult.Payment!.AddDomainEvent(new CreatePaymentEvent(paymentResult.Payment));

            await paymentsRepository.InsertAsync(paymentResult.Payment!, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return paymentResult.Payment!.Id;
    }
}