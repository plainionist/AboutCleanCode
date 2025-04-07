using Microsoft.AspNetCore.Builder;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

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
        new TodoItem("2", "Try out NSwag", true)
    };
});
app.UseOpenApi();

app.Run();

public record TodoItem(string Id, string Title, bool IsDone);
