using FluentValidation;

namespace Barion.Balance.Application.Holds.Commands;

public class MakeHoldCommandValidator : AbstractValidator<MakeHoldCommand>
{
    public MakeHoldCommandValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0);
    }
}