using Microsoft.AspNetCore.Mvc;
using WebApi.OpenApi.Controllers;
using WebApi.OpenApi.Models;

namespace Todo;

public class TodoApiController : DefaultApiController
{
    public override IActionResult GetTodos()
    {
        return Ok(new[]
        {
            new TodoItem {
                Id = "1",
                Title = "Learn type-safe web API",
                IsDone = false
            },
            new TodoItem {
                Id = "2",
                Title = "Try out OpenAPI generator",
                IsDone = true
            }
        });
    }
}
