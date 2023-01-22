namespace WeatherApp.Mediator;

public interface IApplicationRequestHandler<in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
