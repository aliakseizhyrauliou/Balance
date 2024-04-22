using FluentValidation;

namespace Balance.BePaid.Application.PaymentMethods.Queries;

public class GetPaymentMethodByIdQueryValidator : AbstractValidator<GetPaymentMethodByIdQuery>
{
    public GetPaymentMethodByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}