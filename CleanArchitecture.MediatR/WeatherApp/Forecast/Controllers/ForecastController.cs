using Microsoft.AspNetCore.Mvc;
using WeatherApp.Forecast.UseCases;
using WeatherApp.Mediator;

namespace WeatherApp.Forecast.Controllers;

[ApiController]
[Route("[controller]")]
public class ForecastController : ControllerBase
{
    private readonly IApplicationMediator myMediator;

    public ForecastController(IApplicationMediator mediator)
    {
        myMediator = mediator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        var weather = await myMediator.Send<ForecastRequest, IReadOnlyCollection<UseCases.Forecast>>(new ForecastRequest());

        return Ok(weather);
    }
}
