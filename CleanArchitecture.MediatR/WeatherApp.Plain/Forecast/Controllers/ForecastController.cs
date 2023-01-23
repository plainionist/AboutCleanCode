
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Forecast.UseCases;

namespace WeatherApp.Forecast.Controllers;

[ApiController]
[Route("[controller]")]
public class ForecastController : ControllerBase
{
    private readonly IForecastService myForecastService;

    public ForecastController(IForecastService forecastService)
    {
        myForecastService = forecastService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        var weather = await myForecastService.ComputeForecast(new ForecastRequest());

        return Ok(weather);
    }
}
