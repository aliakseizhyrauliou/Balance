using Balance.BePaid.Application.Common.Exceptions;
using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Application.Holds.Commands;
using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Services;
using Moq;

namespace Balance.BePaid.UnitTests.Holds;

public class MakeHoldCommandHandlerTests
{
    private readonly Mock<IPaidResourceTypeRepository> _paidResourceTypeRepository;
    private readonly Mock<IPaymentSystemService> _paymentSystemServiceMock;
    private readonly Mock<IPaymentSystemConfigurationRepository> _paymentSystemConfigurationRepositoryMock;
    private readonly Mock<IHoldRepository> _holdRepositoryMock;
    private readonly Mock<IPaymentMethodRepository> _paymentMethodRepositoryMock;

    public MakeHoldCommandHandlerTests()
    {
        _paymentSystemServiceMock = new Mock<IPaymentSystemService>();
        _paymentSystemConfigurationRepositoryMock = new Mock<IPaymentSystemConfigurationRepository>();
        _holdRepositoryMock = new Mock<IHoldRepository>();
        _paidResourceTypeRepository = new Mock<IPaidResourceTypeRepository>();
        _paymentMethodRepositoryMock = new Mock<IPaymentMethodRepository>();
    }

    [Fact]
    public async Task Handler_Should_Throw_NotFoundException_WhenPaidResourceTypeNotFound()
    {
        var command = new MakeHoldCommand
        {
            UserId = It.IsAny<string>(),
            PaidResourceId = It.IsAny<string>(),
            OperatorId = It.IsAny<string>(),
            Amount = It.IsAny<int>(),
            PaidResourceTypeId = It.IsAny<int>(),
            PaymentMethodId = It.IsAny<int>(),
            PaymentSystemConfigurationId = It.IsAny<int>()
        };

        _paidResourceTypeRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PaidResourceType)null);
        
        var handler = new MakeHoldCommandHandler(_paymentSystemServiceMock.Object,
            _paymentMethodRepositoryMock.Object,
            _holdRepositoryMock.Object,
            _paymentSystemConfigurationRepositoryMock.Object,
            _paidResourceTypeRepository.Object);
        
        await Assert.ThrowsAsync<NotFoundException>(async () =>  await handler.Handle(command, It.IsAny<CancellationToken>()));
    }
}