namespace Athena.Backlog.IO.V1;

using System;
using System.Globalization;
using Athena.Backlog.UseCases;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BacklogController : ControllerBase
{
    private readonly ITeamsRepository myTeamsRepository;
    private readonly BacklogInteractor myInteractor;

    public BacklogController(BacklogInteractor interactor, ITeamsRepository teamsRepository)
    {
        myInteractor = interactor;
        myTeamsRepository = teamsRepository;
    }

    [Route("Backlog/Teams/{name}")]
    public IActionResult GetBacklog(string name, string iteration)
    {
        var team = myTeamsRepository.TryFindByName(name);
        if (team == null)
        {
            return NotFound($"Team not found: {name}");
        }

        if (!TryParseDateTime(iteration, out var iterationStart))
        {
            return BadRequest("Invalid date/time format");
        }

        var response = myInteractor.GetBacklog(new BacklogRequestModel
        {
            Team = team,
            IterationStart = iterationStart
        });

        return Ok(response);
    }

    private static bool TryParseDateTime(string value, out DateTime? result)
    {
        result = null;

        // optional parameters are not treated as error
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        if (DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out var date))
        {
            result = date;
            return true;
        }
        else
        {
            return false;
        }
    }
}
