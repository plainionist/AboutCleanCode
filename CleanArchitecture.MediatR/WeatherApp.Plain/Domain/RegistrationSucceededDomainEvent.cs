using MediatR;

namespace WeatherApp.Domain;

public record RegistrationSucceededDomainEvent(string UserId) : INotification;