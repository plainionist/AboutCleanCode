using MediatR;

namespace WeatherApp.Domain;

internal record RegistrationSucceededDomainEvent(string UserId) : INotification;