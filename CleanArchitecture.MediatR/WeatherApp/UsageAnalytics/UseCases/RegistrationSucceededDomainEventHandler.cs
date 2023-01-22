using WeatherApp.Domain;
using WeatherApp.Mediator;

namespace WeatherApp.UsageAnalytics.UseCases;

class RegistrationSucceededDomainEventHandler : IApplicationNotificationHandler<RegistrationSucceededDomainEvent>
{
    private readonly ILogger<RegistrationSucceededDomainEventHandler> myLogger;

    public RegistrationSucceededDomainEventHandler(ILogger<RegistrationSucceededDomainEventHandler> logger)
    {
        myLogger = logger;
    }

    public Task Handle(RegistrationSucceededDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        // TODO: implement

        myLogger.LogInformation($"Adding registration for user {domainEvent.UserId}");

        return Task.CompletedTask;
    }
}
