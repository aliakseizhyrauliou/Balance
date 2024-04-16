using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using MediatR;

namespace Barion.Balance.Application.PaidResourseTypes.Commands;

public record CreatePaidResourceTypeCommand : IRequest<int>
{
    public required string TypeName { get; set; }
}

public class CreatePaidResourceTypeCommandHandler(IPaidResourceTypeRepository repository) 
    : IRequestHandler<CreatePaidResourceTypeCommand, int>
{
    public async Task<int> Handle(CreatePaidResourceTypeCommand request, CancellationToken cancellationToken)
    {
        var newPaidResourceType = new PaidResourceType
        {
            TypeName = request.TypeName
        };

        await repository.InsertAsync(newPaidResourceType, cancellationToken);

        return newPaidResourceType.Id;
    }
}