using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
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

        if (dbWidget.IsDisabled)
        {
            //TODO Log something
            return;
        }

        var newPaymentMethodServiceResponse = await paymentSystemService.ProcessCreatePaymentMethodPaymentSystemWidgetResponse(request.JsonResponse, dbWidget);

        await UpdatePaymentWidget(dbWidget, newPaymentMethodServiceResponse.IsOk);

        if (newPaymentMethodServiceResponse is { IsOk: true, PaymentMethod: not null })
        {
            await paymentMethodRepository.InsertAsync(newPaymentMethodServiceResponse.PaymentMethod);
        }
    }

    private async Task UpdatePaymentWidget(PaymentSystemWidgetGeneration paymentSystemWidgetGeneration, 
        bool isOk)
    {
        if (!isOk)
        {
            paymentSystemWidgetGeneration.IsSuccess = false;
            paymentSystemWidgetGeneration.IsDisabled = true;
        }
        else
        {
            paymentSystemWidgetGeneration.IsSuccess = true;
            paymentSystemWidgetGeneration.IsDisabled = true;
            paymentSystemWidgetGeneration.IsCompleted = true;
        }
        
        await repository.UpdateAsync(paymentSystemWidgetGeneration);
    }
}