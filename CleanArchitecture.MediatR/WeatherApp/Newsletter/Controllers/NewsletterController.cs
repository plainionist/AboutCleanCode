using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Newsletter.UseCases;

namespace WeatherApp.Newsletter.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsletterController : ControllerBase
{
    private readonly IMediator myMediator;

    public NewsletterController(IMediator mediator)
    {
        myMediator = mediator;
    }

    [HttpPost(Name = "Register")]
    public async Task<IActionResult> Register(string user, string email)
    {
        await myMediator.Send(new RegistrationRequest(user, email));

        return Ok();
    }
}
