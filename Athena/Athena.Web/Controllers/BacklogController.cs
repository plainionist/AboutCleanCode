namespace Athena.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BacklogController : ControllerBase
{
    private Athena.Backlog.Adapters.BacklogController myImpl;
    public BacklogController(Athena.Backlog.Adapters.BacklogController controller)
    {
        myImpl = controller;
    }

    [HttpGet(Name = "Get")]
    public Athena.Backlog.Adapters.BacklogVM GetBacklog(string team)
    {
        return myImpl.GetBacklog(team);
    }
}
