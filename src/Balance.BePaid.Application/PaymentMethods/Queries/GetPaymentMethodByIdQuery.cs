using AutoMapper;
using Balance.BePaid.Application.Common.Exceptions;
using Balance.BePaid.Application.Common.Repositories;
using MediatR;

namespace Balance.BePaid.Application.PaymentMethods.Queries;

public record GetPaymentMethodByIdQuery : IRequest<PaymentMethodDto>
{
    public required int Id { get; set; }
}

public sealed class GetPaymentMethodByIdQueryHandler(IPaymentMethodRepository repository,
    IMapper mapper
    ) : IRequestHandler<GetPaymentMethodByIdQuery, PaymentMethodDto>
{
    public async Task<PaymentMethodDto> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var paymentMethod = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (paymentMethod is null)
        {
            throw new NotFoundException("payment_method_not_found");
        }
        
        return mapper.Map<PaymentMethodDto>(paymentMethod);
    }
}