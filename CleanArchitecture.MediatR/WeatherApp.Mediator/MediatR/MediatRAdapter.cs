using MediatR;

namespace WeatherApp.Mediator.MediatR;

public class MediatRAdapter : IApplicationMediator
{
    private readonly IMediator myImpl;

    public MediatRAdapter(IMediator impl) =>
        myImpl = impl;

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
    {
        return myImpl.Publish(new NotificationAdapter<TNotification>(notification), cancellationToken);
    }

    public Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
    {
        return myImpl.Send(new RequestAdapter<TRequest, TResponse>(request), cancellationToken);
    }
}
