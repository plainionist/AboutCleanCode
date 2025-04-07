using ToDo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddSingleton<IController, ControllerImpl>();

var app = builder.Build();

app.UseCors(policy =>
    policy
        .WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
);
app.MapControllers();

app.Run();

