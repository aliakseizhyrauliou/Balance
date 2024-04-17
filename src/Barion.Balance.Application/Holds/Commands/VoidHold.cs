using Barion.Balance.Application.Common.Exceptions;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Exceptions;
using Barion.Balance.Domain.Services;
using MediatR;

namespace Barion.Balance.Application.Holds.Commands;

/// <summary>
/// Возвращает сумму холда
/// </summary>
public class VoidHoldCommand : IRequest
{
    public required int HoldId { get; set; }
}

public class VoidHoldCommandHandler(IPaymentSystemService paymentSystemService, 
    IHoldRepository holdRepository
    ) : IRequestHandler<VoidHoldCommand>
{
    public async Task Handle(VoidHoldCommand request, CancellationToken cancellationToken)
    {
        var hold = await holdRepository.GetByIdAsync(request.HoldId, cancellationToken);

        if (hold is null)
        {
            throw new NotFoundException("hold_was_not_found");
        }

        var voidHoldPaymentSystemResult = await paymentSystemService.VoidHold(hold, cancellationToken);

        if (!voidHoldPaymentSystemResult.IsOk)
            throw new PaymentSystemException(voidHoldPaymentSystemResult.FriendlyErrorMessage);
        
        
        hold.IsVoided = true;
        await holdRepository.UpdateAsync(hold, cancellationToken);

        throw new PaymentSystemException(voidHoldPaymentSystemResult.FriendlyErrorMessage);
    }
}