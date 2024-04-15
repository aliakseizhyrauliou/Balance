using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Services;
using MediatR;

namespace Barion.Balance.Application.PaymentSystemWidgetGenerations.Commands;

public class ProcessWidgetResponseCommand : IRequest
{
    public string JsonResponse { get; set; }
}

public sealed class ProcessWidgetResponseCommandHandler(IPaymentSystemService paymentSystemService, 
    IPaymentMethodRepository paymentMethodRepository,
    IPaymentSystemWidgetGenerationRepository repository) : IRequestHandler<ProcessWidgetResponseCommand>
{
    public async Task Handle(ProcessWidgetResponseCommand request, CancellationToken cancellationToken)
    {
        var widgetId = await paymentSystemService.GetWidgetId(request.JsonResponse, cancellationToken);

        var dbWidget = await repository.GetByIdAsync(widgetId);

        var newPaymentMethod = await paymentSystemService.ProcessCreatePaymentMethodPaymentSystemWidgetResponse(request.JsonResponse, dbWidget);
        
        await paymentMethodRepository.InsertAsync(newPaymentMethod);
        
    }
}