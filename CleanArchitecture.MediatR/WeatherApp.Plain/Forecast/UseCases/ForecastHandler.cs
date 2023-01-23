
namespace WeatherApp.Forecast.UseCases;

public interface IForecastService
{
    Task<IReadOnlyCollection<Forecast>> ComputeForecast(ForecastRequest request, CancellationToken cancellationToken = default);
}

internal class ForecastHandler : IForecastService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<IReadOnlyCollection<Forecast>> ComputeForecast(ForecastRequest request, CancellationToken cancellationToken)
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
