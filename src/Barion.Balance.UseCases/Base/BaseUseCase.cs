using Barion.Balance.Application.Common.Interfaces;
using MediatR;

namespace Barion.Balance.UseCases.Base;

public abstract class BaseUseCase(IMediator mediator, IUser currentUser) : IBaseUseCase;