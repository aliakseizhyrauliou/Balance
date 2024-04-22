using Balance.BePaid.Application.Common.Exceptions;
using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Entities;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace Balance.BePaid.Application.Debtors.Commands;

public record CreateDebtorCommand : IRequest<int>
{
    public required string UserId { get; set; }

    public required decimal Amount { get; set; }
    public int PaymentMethodId { get; set; }

    /// <summary>
    /// Тип платного ресурса
    /// </summary>
    public int PaidResourceTypeId { get; set; }

    /// <summary>
    /// Платежная система
    /// </summary>
    public int PaymentSystemConfigurationId { get; set; }

    public required string OperatorId { get; set; }
    public required string PaidResourceId { get; set; }
    
    public Dictionary<string, string>? AdditionalData { get; set; }
}

public class CreateDebtorCommandValidator : AbstractValidator<CreateDebtorCommand>
{
    public CreateDebtorCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.PaymentMethodId)
            .NotNull()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PaidResourceTypeId)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.PaymentSystemConfigurationId)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.OperatorId)
            .NotEmpty();

        RuleFor(x => x.PaidResourceId)
            .NotEmpty();
    }
}

public class CreateDebtorCommandHandler(
    IDebtorRepository debtorRepository,
    IPaymentMethodRepository paymentMethodRepository,
    IPaymentSystemConfigurationRepository paymentSystemConfigurationRepository,
    IPaidResourceTypeRepository paidResourceTypeRepository) : IRequestHandler<CreateDebtorCommand, int>
{
    public async Task<int> Handle(CreateDebtorCommand request, 
        CancellationToken cancellationToken)
    {
        
        if(!await paymentMethodRepository.AnyAsync(x => x.Id == request.PaymentMethodId, cancellationToken))
            throw new NotFoundException("payment_method_not_found");
        
        if(!await paidResourceTypeRepository.AnyAsync(x => x.Id == request.PaidResourceTypeId, cancellationToken))
            throw new NotFoundException("paid_resource_not_found");
        
        if(!await paymentSystemConfigurationRepository.AnyAsync(x => x.Id == request.PaymentSystemConfigurationId, cancellationToken))
            throw new Exception("current_payment_system_configuration_not_found");


        var newDebtor = new Debtor
        {
            UserId = request.UserId,
            PaymentMethodId = request.PaymentMethodId,
            PaidResourceTypeId = request.PaidResourceTypeId,
            PaymentSystemConfigurationId = request.PaymentSystemConfigurationId,
            OperatorId = request.OperatorId,
            PaidResourceId = request.PaidResourceId,
            AdditionalData = JsonConvert.SerializeObject(request.AdditionalData),
            CaptureAttemptCount = 0,
            Amount = request.Amount,
            LastCaptureAttempt = null,
            NeedToCapture = true,
        };

        await debtorRepository.InsertAsync(newDebtor, cancellationToken);

        return newDebtor.Id;
    }
    
}

