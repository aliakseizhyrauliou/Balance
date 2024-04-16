using AutoMapper;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Enums;
using Barion.Balance.Domain.Services;
using MediatR;

namespace Barion.Balance.Application.Holds.Commands;

public record MakeHoldCommand : IRequest<int>
{
    public required string UserId { get; set; }
    
    public required int PaidResourceId { get; set; }
    
    public required int OperatorId { get; set; }
    public required decimal Amount { get; set; }

    public required PaidResourceType PaidResourceType { get; set; }
    
    public required int PaymentMethodId { get; set; }    
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
    IMapper mapper)
    : IRequestHandler<MakeHoldCommand, int>
{
    public async Task<int> Handle(MakeHoldCommand request, 
        CancellationToken cancellationToken)
    {
        var domainModel = new Hold
        {
            UserId = request.UserId,
            PaymentMethodId = request.PaymentMethodId,
            PaidResourceId = request.PaidResourceId,
            OperatorId = request.OperatorId,
            Amount = request.Amount,
            PaidResourceType = request.PaidResourceType,
        };
        
        var paymentMethod = await paymentMethodRepository.GetByIdAsync(request.PaymentMethodId, cancellationToken);
        
        //Данный метод ничего не сохранаяет в базу, это не его ответственность
        //Domain слой вообще не должен быть в курсе, что что-то где-то храниться
        var makeHoldRequestToPaymentSystemResult = await paymentSystemService.MakeHold(domainModel, paymentMethod, cancellationToken);

        if (makeHoldRequestToPaymentSystemResult.IsOk)
        {
            await repository.InsertAsync(makeHoldRequestToPaymentSystemResult.Hold, cancellationToken);
        }

        return makeHoldRequestToPaymentSystemResult.Hold.Id;
    }
}