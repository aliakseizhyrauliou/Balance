using Barion.Balance.Application.Common.Interfaces;
using MediatR;

namespace Barion.Balance.UseCases.Base;

public abstract class BaseUseCases(IMediator mediator, IUser currentUser) : IBaseUseCases;