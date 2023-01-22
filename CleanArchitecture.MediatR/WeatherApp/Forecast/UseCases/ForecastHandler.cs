
using WeatherApp.Mediator;

namespace WeatherApp.Forecast.UseCases;

internal class ForecastHandler : IApplicationRequestHandler<ForecastRequest, IReadOnlyCollection<Forecast>>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<IReadOnlyCollection<Forecast>> Handle(ForecastRequest request, CancellationToken cancellationToken)
    {
        var result = Enumerable.Range(1, 5)
            .Select(index => CreateWeatherForecast(DateTime.Now.AddDays(index)))
            .ToReadOnlyCollection();

        return Task.FromResult(result);
    }

    private Forecast CreateWeatherForecast(DateTime date) =>
        new()
        {
            Date = date,
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        };
}
