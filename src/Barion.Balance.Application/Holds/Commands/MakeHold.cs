using System.Data;
using Barion.Balance.Application.Common.Exceptions;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Events.Holds;
using Barion.Balance.Domain.Exceptions;
using Barion.Balance.Domain.Services;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace Barion.Balance.Application.Holds.Commands;

public record MakeHoldCommand : IRequest<int>
{
    public required string UserId { get; set; }
    
    public required string PaidResourceId { get; set; }
    
    public required string OperatorId { get; set; }
    public required decimal Amount { get; set; }

    public required int PaidResourceTypeId { get; set; }
    
    public required int PaymentMethodId { get; set; }

    public Dictionary<string, string>? AdditionalData { get; set; }
}

public class MakeHoldCommandValidator : AbstractValidator<MakeHoldCommand>
{
    public MakeHoldCommandValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0);
    }
}

/// <summary>
/// Холдирует сумму и записывает результат в базу данных
/// </summary>
/// <param name="paymentSystemService"></param>
/// <param name="repository"></param>
/// <param name="mapper"></param>
public sealed class MakeHoldCommandHandler(IPaymentSystemService paymentSystemService, 
    IPaymentMethodRepository paymentMethodRepository,
    IHoldRepository repository,
    IPaymentSystemConfigurationRepository configurationRepository,
    IPaidResourceTypeRepository paidResourceTypeRepository) : IRequestHandler<MakeHoldCommand, int>
{
    public async Task<int> Handle(MakeHoldCommand request, 
        CancellationToken cancellationToken)
    {
        var isPaidResourceTypeExist = await IsPaidResourceTypeExist(request.PaidResourceTypeId, cancellationToken);

        if (!isPaidResourceTypeExist)
        {
            throw new NotFoundException("paid_resource_type_not_found");
        }
        
        //Получаем платежный метод
        var paymentMethod = await paymentMethodRepository.GetByIdAsync(request.PaymentMethodId, cancellationToken);

        if (paymentMethod is null)
        {
            throw new NotFoundException("payment_method_was_not_found");
        }

        var currentPaymentSystemConfiguration = await configurationRepository.GetCurrentSchemaAsync(cancellationToken);

        if (currentPaymentSystemConfiguration is null)
        {
            throw new Exception("current_payment_system_configuration_not_found");
        }
        //Создаем модель 
        var domainModel = new Hold
        {
            UserId = request.UserId,
            PaymentMethodId = request.PaymentMethodId,
            PaidResourceId = request.PaidResourceId,
            OperatorId = request.OperatorId,
            Amount = request.Amount,
            PaidResourceTypeId = request.PaidResourceTypeId,
            AdditionalData = JsonConvert.SerializeObject(request.AdditionalData),
            PaymentSystemConfigurationId = currentPaymentSystemConfiguration.Id
        };
        
        //Данный метод ничего не сохранаяет в базу, это не его ответственность
        //Domain слой вообще не должен быть в курсе, что что-то где-то храниться
        var makeHoldRequestToPaymentSystemResult = await paymentSystemService.Hold(domainModel,
            paymentMethod, 
            currentPaymentSystemConfiguration, 
            cancellationToken);

        if (makeHoldRequestToPaymentSystemResult is { IsOk: true, Hold: not null })
        {
            var hold = makeHoldRequestToPaymentSystemResult.Hold;
            
            await using var transaction = await repository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);

            try
            {
                hold.AddDomainEvent(new MakeHoldEvent(hold));
                await repository.InsertAsync(hold, cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
        else
        {
            throw new PaymentSystemException(makeHoldRequestToPaymentSystemResult.FriendlyErrorMessage);
        }
        
        return makeHoldRequestToPaymentSystemResult.Hold.Id;
    }
    
    private async Task<bool> IsPaidResourceTypeExist(int paidResourceTypeId, 
        CancellationToken cancellationToken)
    {
        return await paidResourceTypeRepository.AnyAsync(x => x.Id == paidResourceTypeId, cancellationToken);
    }
}

