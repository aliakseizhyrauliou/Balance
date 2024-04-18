using System.Data;
using Barion.Balance.Application.Common.Exceptions;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Events.Holds;
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
    IHoldRepository holdRepository,
    IPaymentSystemConfigurationRepository configurationRepository
    ) : IRequestHandler<VoidHoldCommand>
{
    public async Task Handle(VoidHoldCommand request, CancellationToken cancellationToken)
    {
        var hold = await holdRepository.GetByIdAsync(request.HoldId, cancellationToken);

        if (hold is null)
        {
            throw new NotFoundException("hold_was_not_found");
        }
        
        var currentPaymentSystemConfiguration = await configurationRepository.GetCurrentSchemaAsync(cancellationToken);

        if (currentPaymentSystemConfiguration is null)
        {
            throw new Exception("current_payment_system_configuration_not_found");
        }

        var voidHoldPaymentSystemResult = await paymentSystemService.VoidHold(hold, 
            currentPaymentSystemConfiguration, 
            cancellationToken);

        if (!voidHoldPaymentSystemResult.IsOk)
            throw new PaymentSystemException(voidHoldPaymentSystemResult.FriendlyErrorMessage);

        await using var transaction = await holdRepository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);
        try
        {
            voidHoldPaymentSystemResult.Hold!.AddDomainEvent(new VoidHoldEvent(voidHoldPaymentSystemResult.Hold));

            await holdRepository.UpdateAsync(hold, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}