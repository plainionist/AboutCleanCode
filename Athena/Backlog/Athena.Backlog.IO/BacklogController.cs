namespace Athena.Backlog.IO;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BacklogController : ControllerBase
{
    private Athena.Backlog.Adapters.BacklogControllerAdapter myImpl;
    public BacklogController(Athena.Backlog.Adapters.BacklogControllerAdapter controller)
    {
        myImpl = controller;
    }

    [HttpGet(Name = "Get")]
    public Athena.Backlog.Adapters.BacklogVM GetBacklog(string team)
    {
        return myImpl.GetBacklog(team);
    }
}
