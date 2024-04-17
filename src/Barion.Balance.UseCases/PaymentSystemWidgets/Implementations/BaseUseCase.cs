using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.UseCases.PaymentSystemWidgets.Interfaces;
using MediatR;

namespace Barion.Balance.UseCases.PaymentSystemWidgets.Implementations;

public abstract class BaseUseCase(IMediator mediator, IUser currentUser) : IBaseUseCase;