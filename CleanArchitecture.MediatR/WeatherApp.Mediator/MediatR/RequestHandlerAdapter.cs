
using MediatR;

namespace WeatherApp.Mediator.MediatR;

public class RequestHandlerAdapter<TRequest, TResponse> : IRequestHandler<RequestAdapter<TRequest, TResponse>, TResponse>
{
    private readonly IApplicationRequestHandler<TRequest, TResponse> myImpl;

    // injected by DI container
    public RequestHandlerAdapter(IApplicationRequestHandler<TRequest, TResponse> impl)
    {
        myImpl = impl ?? throw new ArgumentNullException(nameof(impl));
    }

    public Task<TResponse> Handle(RequestAdapter<TRequest, TResponse> request, CancellationToken cancellationToken)
    {
        return myImpl.Handle(request.Value, cancellationToken);
    }
}
