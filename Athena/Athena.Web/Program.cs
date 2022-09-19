using Athena.Backlog.Adapters;
using Athena.Backlog.UseCases;
using Athena.Web.Fakes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(typeof(BacklogControllerAdapter));
builder.Services.AddSingleton(typeof(BacklogInteractor));
builder.Services.AddSingleton(typeof(IWorkItemRepository), typeof(FakeWorkItemRepository));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
})); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");
app.UseAuthorization();

app.MapControllers();

app.Run();
