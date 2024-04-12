using AutoMapper;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Application.PaymentMethods.Queries;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Events;
using Barion.Balance.Domain.Events.PaymentMethods;
using MediatR;

namespace Barion.Balance.Application.PaymentMethods.Commands;

public record CreatePaymentMethodCommand : IRequest<PaymentMethodDto>
{
    public required string UserId { get; set; }
}

public sealed class CreatePaymentMethodCommandHandler(IPaymentMethodRepository repository,
    IMapper mapper)
    : IRequestHandler<CreatePaymentMethodCommand, PaymentMethodDto>
{
    public async Task<PaymentMethodDto> Handle(CreatePaymentMethodCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new PaymentMethod
        {
            UserId = request.UserId
        };

        if (await NeedToSelectPaymentMethodAsDefault(request.UserId, cancellationToken))
        {
            entity.IsSelected = true;
        }

        entity.AddDomainEvent(new PaymentMethodCreatedEvent(entity));
        entity.AddDomainEvent(new PaymentMethodSelectedEvent(entity));

        await repository.InsertAsync(entity, cancellationToken);

        return mapper.Map<PaymentMethodDto>(entity);
    }

    private async Task<bool> NeedToSelectPaymentMethodAsDefault(string userId,
        CancellationToken cancellationToken = default)
    {
        return !await repository.AnyAsync(x => x.UserId == userId && x.IsSelected,
            cancellationToken);
    }
}