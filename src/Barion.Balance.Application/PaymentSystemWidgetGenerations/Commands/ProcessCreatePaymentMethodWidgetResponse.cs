using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barion.Balance.Application.PaymentSystemWidgetGenerations.Commands;

/// <summary>
/// Обработка запроса от BePaid по созданию карты
/// </summary>
public class ProcessCreatePaymentMethodWidgetResponseCommand : IRequest
{
    public string JsonResponse { get; set; }
}

public sealed class  ProcessCreatePaymentMethodWidgetResponseCommandHandler(IPaymentSystemService paymentSystemService, 
    IPaymentMethodRepository paymentMethodRepository,
    ILogger<ProcessCreatePaymentMethodWidgetResponseCommandHandler> logger,
    IPaymentSystemWidgetGenerationRepository repository) : IRequestHandler<ProcessCreatePaymentMethodWidgetResponseCommand>
{
    public async Task Handle(ProcessCreatePaymentMethodWidgetResponseCommand request, CancellationToken cancellationToken)
    {
        var widgetId = await paymentSystemService.GetWidgetId(request.JsonResponse, cancellationToken);

        var dbWidget = await repository.GetByIdAsync(widgetId, CancellationToken.None);

        if (dbWidget is null)
        {
            throw new Exception("cannot_find_widget");
        }

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

    private async Task UpdatePaymentWidget(PaymentSystemWidget paymentSystemWidget, 
        bool isOk)
    {
        paymentSystemWidget.GotResponseFromPaymentSystem = true;

        if (!isOk)
        {
            paymentSystemWidget.IsSuccess = false;
            paymentSystemWidget.IsDisabled = true;
        }
        else
        {
            paymentSystemWidget.IsSuccess = true;
            paymentSystemWidget.IsDisabled = true;
        }
        
        await repository.UpdateAsync(paymentSystemWidget);
    }
}