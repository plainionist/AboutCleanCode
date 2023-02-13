using Athena.Backlog.Adapters;
using Microsoft.AspNetCore.Mvc;

namespace Athena.Backlog.IO.V3;

[ApiController]
[Route("[controller]")]
public class BacklogController : ControllerBase
{
    private readonly BacklogControllerAdapter myAdapter;

    public BacklogController(BacklogControllerAdapter adapter)
    {
        myAdapter = adapter;
    }

    [Route("Backlog/Teams/{name}")]
    public IActionResult GetBacklog(string name, string iteration)
    {
        return Ok(myAdapter.GetBacklog(name, iteration));
    }
}
