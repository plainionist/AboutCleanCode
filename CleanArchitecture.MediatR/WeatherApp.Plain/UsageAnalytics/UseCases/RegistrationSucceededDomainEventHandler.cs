using MediatR;
using WeatherApp.Domain;

namespace WeatherApp.UsageAnalytics.UseCases;

class RegistrationSucceededDomainEventHandler : INotificationHandler<RegistrationSucceededDomainEvent>
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
