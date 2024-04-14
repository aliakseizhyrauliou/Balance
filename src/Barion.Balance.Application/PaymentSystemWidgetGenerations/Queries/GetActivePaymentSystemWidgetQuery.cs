using AutoMapper;
using Barion.Balance.Application.Common.Exceptions;
using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using MediatR;

namespace Barion.Balance.Application.PaymentSystemWidgetGenerations.Queries;

public class GetActivePaymentSystemWidgetQuery : IRequest<CheckoutDto>;

public class GetActivePaymentSystemWidgetQueryHandler(IPaymentSystemWidgetGenerationRepository repository, 
    IUser currentUser,
    IMapper mapper) 
    : IRequestHandler<GetActivePaymentSystemWidgetQuery, CheckoutDto>
{
    public async Task<CheckoutDto> Handle(GetActivePaymentSystemWidgetQuery request, 
        CancellationToken cancellationToken)
    {
        if (currentUser.Id is null)
        {
            throw new ForbiddenAccessException("cannot_authorize_user");
        }

        var activeWidget = await repository.GetActiveAsync(currentUser.Id, cancellationToken);

        if (activeWidget is null)
        {
            throw new NotFoundException("widget_not_found");
        }

        return mapper.Map<CheckoutDto>(activeWidget);
    }
}