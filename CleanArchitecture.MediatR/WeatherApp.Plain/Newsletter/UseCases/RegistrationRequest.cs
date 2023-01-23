using MediatR;

namespace WeatherApp.Newsletter.UseCases;

internal record RegistrationRequest(string User, string EMail) : IRequest
{
}