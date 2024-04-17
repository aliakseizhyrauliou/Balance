using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.PaymentSystemWidgetGenerations.Commands;
using Barion.Balance.Application.PaymentSystemWidgetGenerations.Queries;
using Barion.Balance.UseCases.PaymentSystemWidgets.Interfaces;
using MediatR;

namespace Barion.Balance.UseCases.PaymentSystemWidgets.Implementations;

public class PaymentSystemWidgetUseCase(IMediator mediator, IUser currentUser) : BaseUseCase(mediator, currentUser), IPaymentSystemWidgetUseCase
{
    public async Task<CheckoutDto> GeneratePaymentSystemWidget(CancellationToken cancellationToken = default)
    {
        await mediator.Send(new CreatePaymentSystemWidgetGenerationCommand(), cancellationToken);

        var checkout = await mediator.Send(new GetActivePaymentSystemWidgetQuery(), cancellationToken);

        return checkout;
    }
}