using System.Data;
using Barion.Balance.Application.Common.Exceptions;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Events.Payments;
using Barion.Balance.Domain.Exceptions;
using Barion.Balance.Domain.Services;
using MediatR;

namespace Barion.Balance.Application.Payments.Commands;

public class RefundPaymentCommand : IRequest
{
    public required int PaymentId { get; set; }
}

public class RefundPaymentCommandHandler(IPaymentSystemService paymentSystemService,
    IPaymentRepository paymentRepository,
    IPaymentSystemConfigurationRepository paymentSystemConfigurationRepository) 
    : IRequestHandler<RefundPaymentCommand>
{
    public async Task Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);

        if (payment is null)
        {
            throw new NotFoundException("payment_was_not_found");
        }
        
        var currentPaymentSystemConfiguration = await paymentSystemConfigurationRepository.GetCurrentSchemaAsync(cancellationToken);

        if (currentPaymentSystemConfiguration is null)
        {
            throw new Exception("current_payment_system_configuration_not_found");
        }

        var refundPaymentResult =
            await paymentSystemService.Refund(payment, currentPaymentSystemConfiguration, cancellationToken);

        if (refundPaymentResult is { IsOk: false })
            throw new PaymentSystemException(refundPaymentResult.FriendlyErrorMessage);
        
        await using var transaction = await paymentRepository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);
        try
        {
            var refundedPayment = refundPaymentResult.Payment;

            refundedPayment.AddDomainEvent(new RefundPaymentEvent(refundedPayment, refundPaymentResult.RefundPaymentUrl));

            await paymentRepository.UpdateAsync(refundedPayment, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}