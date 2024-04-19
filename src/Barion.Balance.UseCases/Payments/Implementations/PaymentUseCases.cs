using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.PaymentMethods.Queries;
using Barion.Balance.Application.Payments.Commands;
using Barion.Balance.UseCases.Base;
using Barion.Balance.UseCases.Payments.Dtos;
using Barion.Balance.UseCases.Payments.Interfaces;
using Barion.Balance.UseCases.PaymentSystemWidgets.Implementations;
using MediatR;

namespace Barion.Balance.UseCases.Payments.Implementations;

public class PaymentUseCases(IMediator mediator, IUser currentUser) : BaseUseCase(mediator, currentUser), IPaymentUseCases
{
    public async Task PaymentWithSelectedPaymentMethod(PaymentWithSelectedPaymentMethodDto dto,
        CancellationToken cancellationToken = default)
    {
        var selectedMethod = await mediator.Send(new GetSelectedPaymentMethodByUserIdQuery
        {
            UserId = currentUser.Id
        }, cancellationToken);

        await mediator.Send(new CreatePaymentCommand
        {
            UserId = currentUser.Id,
            Amount = dto.Amount,
            PaidResourceId = dto.PaidResourceId,
            AdditionalData = dto.AdditionalData,
            OperatorId = dto.OperatorId,
            IsBonus = false,
            PaymentMethodId = selectedMethod.Id,
            PaidResourceTypeId = dto.PaidResourceTypeId,
        }, cancellationToken);
    }
}