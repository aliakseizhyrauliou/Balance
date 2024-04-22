using AutoMapper;
using Balance.BePaid.Application.Common.Exceptions;
using Balance.BePaid.Application.Common.Interfaces;
using Balance.BePaid.Application.Common.Repositories;
using MediatR;

namespace Balance.BePaid.Application.PaymentMethods.Queries;

public record GetSelectedPaymentMethodByUserIdQuery : IRequest<PaymentMethodDto>
{
    public required string UserId { get; set; }
}

public class GetSelectedPaymentMethodByUserIdQueryHandler(IPaymentMethodRepository paymentMethodRepository,
    IMapper mapper,
    IUser currentUser) 
    : IRequestHandler<GetSelectedPaymentMethodByUserIdQuery, PaymentMethodDto>
{
    public async Task<PaymentMethodDto> Handle(GetSelectedPaymentMethodByUserIdQuery request, CancellationToken cancellationToken)
    {
        var paymentMethod = await paymentMethodRepository.GetSelectedAsync(currentUser.Id, cancellationToken);

        if (paymentMethod is null)
            throw new NotFoundException("selected_payment_method_not_found");

        return mapper.Map<PaymentMethodDto>(paymentMethod);
    }
}