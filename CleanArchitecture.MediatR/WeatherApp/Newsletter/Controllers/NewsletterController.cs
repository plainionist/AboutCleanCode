using Microsoft.AspNetCore.Mvc;
using WeatherApp.Mediator;
using WeatherApp.Newsletter.UseCases;

namespace WeatherApp.Newsletter.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsletterController : ControllerBase
{
    private readonly IApplicationMediator myMediator;

    public NewsletterController(IApplicationMediator mediator)
    {
        myMediator = mediator;
    }

    [HttpPost(Name = "Register")]
    public async Task<IActionResult> Register(string user, string email)
    {
        await myMediator.Send<RegistrationRequest, Unit>(new RegistrationRequest(user, email));

        return Ok();
    }
}
