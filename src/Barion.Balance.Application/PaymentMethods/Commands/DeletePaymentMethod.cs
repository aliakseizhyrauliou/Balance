using Barion.Balance.Application.Common.Exceptions;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using MediatR;

namespace Barion.Balance.Application.PaymentMethods.Commands;

public record DeletePaymentMethodCommand : IRequest
{
    public required int PaymentMethodId { get; set; }
    public required string UserId { get; set; }
}

public sealed class DeletePaymentMethodCommandHandler(IPaymentMethodRepository repository) 
    : IRequestHandler<DeletePaymentMethodCommand>
{
    public async Task Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = await repository.GetByIdAsync(request.PaymentMethodId, cancellationToken);

        if (paymentMethod is null)
        {
            throw new NotFoundException("payment_method_not_found");
        }

        if (!IsUserOwnPaymentMethod(paymentMethod, request))
        {
            throw new ForbiddenAccessException("user_doesnt_own_this_payment_method");
        }

        await repository.DeleteAsync(paymentMethod, cancellationToken);
    }
    
    private bool IsUserOwnPaymentMethod(PaymentMethod paymentMethod, 
        DeletePaymentMethodCommand request)
    {
        return paymentMethod.UserId.Equals(request.UserId);
    }
}