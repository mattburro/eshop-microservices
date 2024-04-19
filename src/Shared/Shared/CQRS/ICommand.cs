using MediatR;

namespace Shared.CQRS;

public interface ICommand<out TResult> : IRequest<TResult> { }

public interface ICommand : IRequest { }
