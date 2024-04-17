using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barion.Balance.Application.PaymentSystemWidgetGenerations.Commands;

public class ProcessWidgetResponseCommand : IRequest
{
    public string JsonResponse { get; set; }
}

public sealed class ProcessWidgetResponseCommandHandler(IPaymentSystemService paymentSystemService, 
    IPaymentMethodRepository paymentMethodRepository,
    ILogger<ProcessWidgetResponseCommandHandler> logger,
    IPaymentSystemWidgetGenerationRepository repository) : IRequestHandler<ProcessWidgetResponseCommand>
{
    public async Task Handle(ProcessWidgetResponseCommand request, CancellationToken cancellationToken)
    {
        var widgetId = await paymentSystemService.GetWidgetId(request.JsonResponse, cancellationToken);

        var dbWidget = await repository.GetByIdAsync(widgetId, CancellationToken.None);

        if (dbWidget.IsDisabled)
        {
            logger.LogWarning($"Payment system webhook request to disabled Widget. WidgetId = {dbWidget.Id}. PaymentSystemRequest = {request.JsonResponse}");
            return;
        }

        var newPaymentMethodServiceResponse = await paymentSystemService.ProcessCreatePaymentMethodPaymentSystemWidgetResponse(request.JsonResponse, dbWidget, CancellationToken.None);

        await UpdatePaymentWidget(dbWidget, newPaymentMethodServiceResponse.IsOk);

        if (newPaymentMethodServiceResponse is { IsOk: true, PaymentMethod: not null })
        {
            await paymentMethodRepository.InsertAsync(newPaymentMethodServiceResponse.PaymentMethod, CancellationToken.None);
        }
    }

    private async Task UpdatePaymentWidget(PaymentSystemWidgetGeneration paymentSystemWidgetGeneration, 
        bool isOk)
    {
        paymentSystemWidgetGeneration.GotResponseFromPaymentSystem = true;

        if (!isOk)
        {
            paymentSystemWidgetGeneration.IsSuccess = false;
            paymentSystemWidgetGeneration.IsDisabled = true;
        }
        else
        {
            paymentSystemWidgetGeneration.IsSuccess = true;
            paymentSystemWidgetGeneration.IsDisabled = true;
        }
        
        await repository.UpdateAsync(paymentSystemWidgetGeneration);
    }
}