using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Balance.BePaid.Application.PaidResourseTypes.Commands;

public record CreatePaidResourceTypeCommand : IRequest<int>
{
    public required string TypeName { get; set; }
}

public class CreatePaidResourceTypeCommandValidator : AbstractValidator<CreatePaidResourceTypeCommand>
{
    public CreatePaidResourceTypeCommandValidator()
    {
        RuleFor(x => x.TypeName)
            .NotNull();
    }
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