using AutoMapper;
using Barion.Balance.Application.Common.Repositories;
using MediatR;

namespace Barion.Balance.Application.PaidResourseTypes.Queries;

public record GetPaidResourceTypesQuery : IRequest<List<PaidResourceTypeDto>>;

public class GetPaidResourceTypesQueryHandler(IMapper mapper, 
    IPaidResourceTypeRepository repository) 
    : IRequestHandler<GetPaidResourceTypesQuery, List<PaidResourceTypeDto>>
{
    public async Task<List<PaidResourceTypeDto>> Handle(GetPaidResourceTypesQuery request, CancellationToken cancellationToken)
    {
        var paidResources = await repository.GetListAsync(cancellationToken);

        return mapper.Map<List<PaidResourceTypeDto>>(paidResources);
    }
}