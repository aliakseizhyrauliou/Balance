using Barion.Balance.Application.Common.Exceptions;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Application.Holds.Commands;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Exceptions;
using Barion.Balance.Domain.Services;
using Barion.Balance.Domain.Services.ServiceResponses;
using Moq;

namespace Barion.Balance.UnitTests.Holds;

public class CaptureHoldCommandHandlerTests
{
    private readonly Mock<IPaymentSystemService> _paymentSystemServiceMock;
    private readonly Mock<IPaymentSystemConfigurationRepository> _paymentSystemConfigurationRepositoryMock;
    private readonly Mock<IHoldRepository> _holdRepositoryMock;
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;

    public CaptureHoldCommandHandlerTests()
    {
        _paymentSystemServiceMock = new Mock<IPaymentSystemService>();
        _paymentSystemConfigurationRepositoryMock = new Mock<IPaymentSystemConfigurationRepository>();
        _holdRepositoryMock = new Mock<IHoldRepository>();
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
    }
    
    [Fact]
    public async Task Handler_Should_Throw_NotFoundException_WhenHoldNotFound()
    {
        var command = new CaptureHoldCommand
        {
            HoldId = It.IsAny<int>()
        };

        _holdRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Hold?)null);
        
        var handler = new CaptureHoldCommandHandler(_paymentSystemServiceMock.Object,
            _paymentSystemConfigurationRepositoryMock.Object,
            _holdRepositoryMock.Object,
            _paymentRepositoryMock.Object);
        
        await Assert.ThrowsAsync<NotFoundException>(async () =>  await handler.Handle(command, It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task Handler_Should_Throw_InvalidArgumentException_WhenHoldAmountEqualZero()
    {
        var command = new CaptureHoldCommand
        {
            HoldId = It.IsAny<int>()
        };

        _holdRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Hold
            {
                UserId = It.IsAny<string>(),
                PaidResourceId = It.IsAny<string>(),
                OperatorId = It.IsAny<string>(),
                PaymentSystemConfigurationId = It.IsAny<int>(),
                Amount = 0
            });
        
        var handler = new CaptureHoldCommandHandler(_paymentSystemServiceMock.Object,
            _paymentSystemConfigurationRepositoryMock.Object,
            _holdRepositoryMock.Object,
            _paymentRepositoryMock.Object);
        
        await Assert.ThrowsAsync<InvalidArgumentException>(async () =>  await handler.Handle(command, It.IsAny<CancellationToken>()));
    }
    
    [Fact]
    public async Task Handler_Should_Throw_Exception_WhenCurrentPaymentSystemConfigurationNotFound()
    {
        var command = new CaptureHoldCommand
        {
            HoldId = It.IsAny<int>()
        };

        _holdRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Hold
            {
                UserId = It.IsAny<string>(),
                PaidResourceId = It.IsAny<string>(),
                OperatorId = It.IsAny<string>(),
                PaymentSystemConfigurationId = It.IsAny<int>(),
                Amount = 10
            });
        
        _paymentSystemConfigurationRepositoryMock
            .Setup(x => x.GetCurrentSchemaAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((PaymentSystemConfiguration)null);
        
        var handler = new CaptureHoldCommandHandler(_paymentSystemServiceMock.Object,
            _paymentSystemConfigurationRepositoryMock.Object,
            _holdRepositoryMock.Object,
            _paymentRepositoryMock.Object);
        
        await Assert.ThrowsAsync<Exception>(async () =>  await handler.Handle(command, It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task Handler_Should_Throw_PaymentSystemException_WhenCaptureHoldIsNotOk()
    {
        var command = new CaptureHoldCommand
        {
            HoldId = It.IsAny<int>()
        };

        _holdRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Hold
            {
                UserId = It.IsAny<string>(),
                PaidResourceId = It.IsAny<string>(),
                OperatorId = It.IsAny<string>(),
                PaymentSystemConfigurationId = It.IsAny<int>(),
                Amount = 10
            });
        
        _paymentSystemConfigurationRepositoryMock
            .Setup(x => x.GetCurrentSchemaAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PaymentSystemConfiguration
            {
                PaymentSystemName = It.IsAny<string>(),
                Data = It.IsAny<string>()
            });

        _paymentSystemServiceMock
            .Setup(x => x.CaptureHold(It.IsAny<Hold>(), It.IsAny<PaymentSystemConfiguration>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProcessCaptureHoldPaymentSystemResult
            {
                IsOk = false
            });
        
        var handler = new CaptureHoldCommandHandler(_paymentSystemServiceMock.Object,
            _paymentSystemConfigurationRepositoryMock.Object,
            _holdRepositoryMock.Object,
            _paymentRepositoryMock.Object);
        
        await Assert.ThrowsAsync<PaymentSystemException>(async () =>  await handler.Handle(command, It.IsAny<CancellationToken>()));
    }
    
    [Fact]
    public async Task Handler_Should_Throw_Exception_WhenCaptureHoldIsOkAndHoldIsNull()
    {
        var command = new CaptureHoldCommand
        {
            HoldId = It.IsAny<int>()
        };

        _holdRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Hold
            {
                UserId = It.IsAny<string>(),
                PaidResourceId = It.IsAny<string>(),
                OperatorId = It.IsAny<string>(),
                PaymentSystemConfigurationId = It.IsAny<int>(),
                Amount = 10
            });
        
        _paymentSystemConfigurationRepositoryMock
            .Setup(x => x.GetCurrentSchemaAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PaymentSystemConfiguration
            {
                PaymentSystemName = It.IsAny<string>(),
                Data = It.IsAny<string>()
            });

        _paymentSystemServiceMock
            .Setup(x => x.CaptureHold(It.IsAny<Hold>(), It.IsAny<PaymentSystemConfiguration>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProcessCaptureHoldPaymentSystemResult
            {
                IsOk = true,
                Hold = null
            });
        
        var handler = new CaptureHoldCommandHandler(_paymentSystemServiceMock.Object,
            _paymentSystemConfigurationRepositoryMock.Object,
            _holdRepositoryMock.Object,
            _paymentRepositoryMock.Object);
        
        await Assert.ThrowsAsync<Exception>(async () =>  await handler.Handle(command, It.IsAny<CancellationToken>()));
    }

}