using Todo.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy =>
    policy
        .WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.MapGet("/todos", () =>
{
    return new[]
    {
        new TodoItem {
            Id = "1",
            Title = "Learn type-safe web API",
            IsDone = false
        },
        new TodoItem {
            Id = "2",
            Title = "Try out manually",
            IsDone = true
        }
    };
});

app.Run();
