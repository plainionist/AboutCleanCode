var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton(typeof(Athena.Backlog.Adapters.BacklogController));
builder.Services.AddSingleton(typeof(Athena.Backlog.UseCases.BacklogInteractor));
builder.Services.AddSingleton(typeof(Athena.Backlog.UseCases.IWorkItemRepository), typeof(Athena.Web.Fakes.FakeWorkItemRepository));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
