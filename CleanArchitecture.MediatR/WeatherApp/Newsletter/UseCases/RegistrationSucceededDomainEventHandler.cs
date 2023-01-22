using MediatR;
using WeatherApp.Domain;

namespace WeatherApp.Newsletter.UseCases;

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

        myLogger.LogInformation($"Sending welcome email for user {domainEvent.UserId}");

        return Task.CompletedTask;
    }
}
