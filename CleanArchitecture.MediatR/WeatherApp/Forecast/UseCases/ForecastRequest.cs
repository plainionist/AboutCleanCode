using MediatR;

namespace WeatherApp.Forecast.UseCases;

internal class ForecastRequest : IRequest<IReadOnlyCollection<Forecast>>
{
}