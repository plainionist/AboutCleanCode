
namespace ToDo;

public class ControllerImpl : IController
{
    public Task<ICollection<TodoItem>> GetTodosAsync()
    {
        return Task.FromResult<ICollection<TodoItem>>(
        [
            new TodoItem { Id = "1", Title = "Learn type-safe web API", IsDone = false },
            new TodoItem { Id = "2", Title = "Try out NSwag schema-first", IsDone = true }
        ]);
    }
}
