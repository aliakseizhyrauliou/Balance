using System.Data;
using Barion.Balance.Application.Common.Exceptions;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Exceptions;
using Barion.Balance.Domain.Services;
using FluentValidation;
using MediatR;

namespace Barion.Balance.Application.Holds.Commands;

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
    IHoldRepository holdRepository,
    IAccountRecordRepository accountRecordRepository) 
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

        var captureHoldPaymentSystemResult = await paymentSystemService.CaptureHold(hold, cancellationToken);

        if (!captureHoldPaymentSystemResult.IsOk)
            throw new PaymentSystemException(captureHoldPaymentSystemResult.FriendlyErrorMessage);

        if (captureHoldPaymentSystemResult is { Hold: null })
            throw new Exception("hold_was_null");


        if (captureHoldPaymentSystemResult is {NeedToCreateAccountRecord: true, Payment: not null})
        {
            await using var transaction = await holdRepository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);
            
            try
            {
                await holdRepository.UpdateAsync(captureHoldPaymentSystemResult.Hold!, cancellationToken);
                await accountRecordRepository.InsertAsync(captureHoldPaymentSystemResult.Payment,
                    cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                transaction.RollbackAsync(cancellationToken);
                throw new Exception($"error_while_saving_capture_hold. Message = {ex.Message}");
            }
        }

        await holdRepository.UpdateAsync(hold, cancellationToken);
    }
} 