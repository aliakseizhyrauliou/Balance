using FluentValidation;

namespace Barion.Balance.Application.PaymentMethods.Queries;

public class GetPaymentMethodByIdQueryValidator : AbstractValidator<GetPaymentMethodByIdQuery>
{
    public GetPaymentMethodByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}