using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Forecast.UseCases;

namespace WeatherApp.Forecast.Controllers;

[ApiController]
[Route("[controller]")]
public class ForecastController : ControllerBase
{
    private readonly IMediator myMediator;

    public ForecastController(IMediator mediator)
    {
        myMediator = mediator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        var weather = await myMediator.Send(new ForecastRequest());

        return Ok(weather);
    }
}
