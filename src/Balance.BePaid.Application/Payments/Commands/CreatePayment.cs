using System.Data;
using Balance.BePaid.Application.Common.Exceptions;
using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Events.Payments;
using Balance.BePaid.Domain.Exceptions;
using Balance.BePaid.Domain.Services;
using MediatR;
using Newtonsoft.Json;

namespace Balance.BePaid.Application.Payments.Commands;

public class CreatePaymentCommand : IRequest<int>
{
    /// <summary>
    /// Идентификатор пользователя 
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Сумма платежа
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    ///Id того, за что была оплата
    ///Id бронирования, зарядки или парковки
    /// </summary>
    public required string PaidResourceId { get; set; }

    /// <summary>
    /// Специфическая инфа платежа.
    /// </summary>
    public Dictionary<string, string>? AdditionalData { get; set; }
    

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
    public  int PaymentMethodId { get; set; }

    public  int PaidResourceTypeId { get; set; }
    
    public required int PaymentSystemConfigurationId { get; set; }
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

        var paymentSystemConfiguration =
            await paymentSystemConfigurationRepository.GetByIdAsync(request.PaymentSystemConfigurationId,
                cancellationToken);

        if (paymentSystemConfiguration is null)
        {
            throw new NotFoundException("payment_system_configuration_not_found");
        }

        var payment = new Payment
        {
            UserId = request.UserId,
            Amount = request.Amount,
            PaidResourceId = request.PaidResourceId,
            PaymentSystemTransactionId = null,
            OperatorId = request.OperatorId,
            PaymentMethodId = request.PaymentMethodId,
            PaidResourceTypeId = request.PaidResourceTypeId,
            AdditionalData = JsonConvert.SerializeObject(request.AdditionalData),
            PaymentSystemConfigurationId = paymentSystemConfiguration.Id
        };

        var paymentResult = await paymentSystemService.Payment(payment, 
            paymentMethod, 
            paymentSystemConfiguration, 
            cancellationToken);

        if (paymentResult is not { IsOk: true, Payment: not null })
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