using Balance.BePaid.Application.Common.Interfaces;
using MediatR;

namespace Balance.BePaid.UseCases.Base;

public abstract class BaseUseCases(IMediator mediator, IUser currentUser) : IBaseUseCases;