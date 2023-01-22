namespace WeatherApp.Mediator;

public interface IApplicationNotificationHandler<in T>
{
    Task Handle(T notification, CancellationToken cancellationToken);
}
