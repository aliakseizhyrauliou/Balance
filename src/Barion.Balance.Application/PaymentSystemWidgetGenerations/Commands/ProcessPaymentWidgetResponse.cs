using System.Data;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Events.Payments;
using Barion.Balance.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barion.Balance.Application.PaymentSystemWidgetGenerations.Commands;

/// <summary>
/// Обработка запроса платежной системы об оплате
/// </summary>
public class ProcessPaymentWidgetResponseCommand : IRequest
{
    public string? JsonResponse { get; set; }
}

public sealed class ProcessPaymentWidgetResponseCommandHandler(
    IPaymentSystemService paymentSystemService,
    IPaymentRepository paymentRepository,
    ILogger<ProcessPaymentWidgetResponseCommandHandler> logger,
    IPaymentSystemWidgetGenerationRepository widgetRepository) : IRequestHandler<ProcessPaymentWidgetResponseCommand>
{
    public async Task Handle(ProcessPaymentWidgetResponseCommand request, CancellationToken cancellationToken)
    {
        var widgetId = await paymentSystemService.GetWidgetId(request.JsonResponse, CancellationToken.None);

        var dbWidget = await widgetRepository.GetByIdAsync(widgetId, CancellationToken.None);

        if (dbWidget is null)
        {
            throw new Exception("cannot_find_widget");
        }

        if (dbWidget.IsDisabled)
        {
            logger.LogWarning($"Payment system webhook request to disabled Widget. WidgetId = {dbWidget.Id}. PaymentSystemRequest = {request.JsonResponse}");
            return;
        }

        var paymentFromWidgetResponse = await paymentSystemService.ProcessPaymentSystemWidgetResponse(request.JsonResponse, 
            dbWidget, 
            CancellationToken.None);


        if (paymentFromWidgetResponse is { IsOk: true, Payment: not null })
        {
            await using var transaction = await paymentRepository.BeginTransaction(IsolationLevel.ReadCommitted, CancellationToken.None);

            try
            {
                var payment = paymentFromWidgetResponse.Payment;
                var updatedWidget = paymentFromWidgetResponse.PaymentSystemWidget;

                await UpdatePaymentWidget(updatedWidget, paymentFromWidgetResponse.IsOk);

                payment.AddDomainEvent(new CreatePaymentEvent(payment));
                payment.PaymentSystemWidgetId = updatedWidget.Id;

                await paymentRepository.InsertAsync(payment, cancellationToken);

                await transaction.CommitAsync(CancellationToken.None);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(CancellationToken.None);
                throw;
            }
        }
    }
    
    private async Task UpdatePaymentWidget(
        PaymentSystemWidget paymentSystemWidget, 
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
        
        await widgetRepository.UpdateAsync(paymentSystemWidget);
    }
}