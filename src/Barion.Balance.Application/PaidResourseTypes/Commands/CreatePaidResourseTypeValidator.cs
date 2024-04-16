using FluentValidation;

namespace Barion.Balance.Application.PaidResourseTypes.Commands;

public class CreatePaidResourseTypeValidator : AbstractValidator<CreatePaidResourceTypeCommand>
{
    public CreatePaidResourseTypeValidator()
    {
        RuleFor(x => x.TypeName)
            .NotNull();
    }
}