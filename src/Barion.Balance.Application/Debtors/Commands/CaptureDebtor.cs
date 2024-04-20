using System.Data;
using Barion.Balance.Application.Common.Exceptions;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Events.Payments;
using Barion.Balance.Domain.Exceptions;
using Barion.Balance.Domain.Services;
using Barion.Balance.Domain.Services.ServiceResponses;
using MediatR;

namespace Barion.Balance.Application.Debtors.Commands;

public class CaptureDebtorCommand : IRequest<int>
{
    public required int DebtorId { get; set; }
}

public class CaptureDebtorCommandHandler(
    IPaymentSystemConfigurationRepository paymentSystemConfigurationRepository,
    IPaymentMethodRepository paymentMethodRepository,
    IPaymentRepository paymentRepository,
    IPaymentSystemService paymentSystemService,
    IDebtorRepository debtorRepository)
    : IRequestHandler<CaptureDebtorCommand, int>
{
    public async Task<int> Handle(CaptureDebtorCommand request, CancellationToken cancellationToken)
    {
        var debtor = await debtorRepository.GetByIdAsync(request.DebtorId, cancellationToken);

        if (debtor is null)
        {
            throw new NotFoundException("debtor_not_found");
        }

        if (!debtor.NeedToCapture)
        {
            throw new InvalidArgumentException("need_to_capture_unmarked");
        }

        var paymentMethod = await paymentMethodRepository.GetByIdAsync(debtor.PaymentMethodId, cancellationToken);

        if (paymentMethod is null)
        {
            throw new NotFoundException("payment_method_not_found");
        }
        
        var paymentSystemConfiguration =
            await paymentSystemConfigurationRepository.GetByIdAsync(debtor.PaymentSystemConfigurationId,
                cancellationToken);

        if (paymentSystemConfiguration is null)
        {
            throw new NotFoundException("payment_system_configuration_not_found");
        }
        
        var payment = new Payment
        {
            UserId = debtor.UserId!,
            Amount = debtor.Amount,
            PaidResourceId = debtor.PaidResourceId!,
            PaymentSystemTransactionId = null,
            OperatorId = debtor.OperatorId,
            PaymentMethodId = debtor.PaymentMethodId,
            PaidResourceTypeId = debtor.PaidResourceTypeId,
            AdditionalData = debtor.AdditionalData,
            PaymentSystemConfigurationId = debtor.PaymentSystemConfigurationId
        };


        var paymentResult = new ProcessPaymentPaymentSystemResult
        {
            IsOk = false
        };

        try
        {
            
            debtor.CaptureAttemptCount++;
            debtor.LastCaptureAttempt = DateTimeOffset.UtcNow;
            
            paymentResult = await paymentSystemService.Payment(payment,
                paymentMethod,
                paymentSystemConfiguration,
                cancellationToken);
        }
        catch (PaymentSystemException exception)
        {
            debtor.DebtorCaptureLastErrorMessage = exception.Message;
            
            await debtorRepository.UpdateAsync(debtor, cancellationToken);

            throw;
        }
        
        if (paymentResult is not { IsOk: true, Payment: not null })
        {
            debtor.DebtorCaptureLastErrorMessage = paymentResult.ErrorMessage;
            
            await debtorRepository.UpdateAsync(debtor, cancellationToken);
            throw new PaymentSystemException(paymentResult.FriendlyErrorMessage);
        }
        
        await using var transaction = await paymentRepository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);

        try
        {
            paymentResult.Payment!.AddDomainEvent(new CreatePaymentEvent(paymentResult.Payment));
            payment.CaptureDebtorId = debtor.Id;
            
            await paymentRepository.InsertAsync(paymentResult.Payment!, cancellationToken);

            debtor.IsCaptured = true;
            debtor.NewPaymentId = paymentResult.Payment!.Id;
            
            await debtorRepository.UpdateAsync(debtor, cancellationToken);
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