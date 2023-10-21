using MediatR;

namespace Sparks.Api.Shared;

public interface ICommand<TResponse> : IRequest<TResponse>
{
    
}
