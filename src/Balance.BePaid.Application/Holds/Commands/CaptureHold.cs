using System.Data;
using Balance.BePaid.Application.Common.Exceptions;
using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Events.Payments;
using Balance.BePaid.Domain.Exceptions;
using Balance.BePaid.Domain.Services;
using FluentValidation;
using MediatR;

namespace Balance.BePaid.Application.Holds.Commands;

public class CaptureHoldCommand : IRequest
{
    public required int HoldId { get; set; }
}

public class CaptureHoldCommandValidator : AbstractValidator<CaptureHoldCommand>
{
    public CaptureHoldCommandValidator()
    {
        RuleFor(x => x.HoldId)
            .NotEmpty();
    }
}

public class CaptureHoldCommandHandler(IPaymentSystemService paymentSystemService, 
    IPaymentSystemConfigurationRepository paymentSystemConfigurationRepository,
    IHoldRepository holdRepository,
    IPaymentRepository paymentRepository) 
    : IRequestHandler<CaptureHoldCommand>
{
    public async Task Handle(CaptureHoldCommand request, CancellationToken cancellationToken)
    {
        var hold = await holdRepository.GetByIdAsync(request.HoldId, cancellationToken);

        if (hold is null)
        {
            throw new NotFoundException("hold_not_found");
        }

        if (hold.Amount == 0)
        {
            throw new InvalidArgumentException("cannot_capture_hold_with_zero_amount");
        }
        
        var currentPaymentSystemConfiguration = await paymentSystemConfigurationRepository.GetCurrentSchemaAsync(cancellationToken);

        if (currentPaymentSystemConfiguration is null)
        {
            throw new Exception("current_payment_system_configuration_not_found");
        }

        var captureHoldPaymentSystemResult = await paymentSystemService.CaptureHold(hold, currentPaymentSystemConfiguration, cancellationToken);

        if (!captureHoldPaymentSystemResult.IsOk)
            throw new PaymentSystemException(captureHoldPaymentSystemResult.FriendlyErrorMessage);

        if (captureHoldPaymentSystemResult is { Hold: null })
            throw new Exception("hold_was_null");
        
        
        if (captureHoldPaymentSystemResult is {NeedToCreatePaymentRecord: true, Payment: not null})
        {
            var capturedHold = captureHoldPaymentSystemResult.Hold!;
            var newPayment = captureHoldPaymentSystemResult.Payment!;
            
            await using var transaction = await holdRepository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);
            
            try
            {
                //Создание чека
                newPayment.AddDomainEvent(new CreatePaymentEvent(newPayment));
                
                await paymentRepository.InsertAsync(captureHoldPaymentSystemResult.Payment,
                    cancellationToken);

                await holdRepository.UpdateAsync(capturedHold, 
                    cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"error_while_saving_capture_hold. Message = {ex.Message}");
            }
        }

        await holdRepository.UpdateAsync(hold, cancellationToken);
    }
} 