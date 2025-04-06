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
        new TodoItem("1", "Learn type-safe web API", false),
        new TodoItem("2", "Try out manually", true)
    };
});

app.Run();
