using AutoMapper;
using AutoMapper.QueryableExtensions;
using Barion.Balance.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Barion.Balance.Application.PaymentMethods.Queries;

public class GetPaymentMethodsListQuery : IRequest<List<PaymentMethodDto>>;

public class GetPaymentMethodsListQueryHandler(IUser user, IBalanceDbContext balanceDbContext, IMapper mapper) : IRequestHandler<GetPaymentMethodsListQuery, List<PaymentMethodDto>>
{
    public async Task<List<PaymentMethodDto>> Handle(GetPaymentMethodsListQuery request, CancellationToken cancellationToken)
    {
        return await balanceDbContext.PaymentMethods
            .ProjectTo<PaymentMethodDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}