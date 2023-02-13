using Athena.Backlog.Adapters;
using Athena.Backlog.Adapters.TestApi;
using Athena.Backlog.IO;
using Athena.Backlog.UseCases;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(p => p.AddPolicy("CorsApp", builder =>
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register application types 
builder.Services.AddSingleton(typeof(BacklogControllerAdapter));
builder.Services.AddSingleton(typeof(BacklogInteractor));
builder.Services.AddSingleton(typeof(IWorkItemRepository), typeof(FakeWorkItemRepository));
builder.Services.AddSingleton(typeof(ITeamsRepository), typeof(FakeTeamsRepository));

// register exception filter
builder.Services.AddControllers(options =>
    options.Filters.Add<ControllerExceptionFilter>());

var app = builder.Build();

// define Web Api
app.MapGet("/backlog/teams/{name}", (string name, string iteration, BacklogControllerAdapter adapter) =>
    adapter.GetBacklog(name, iteration));










if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.UseCors("CorsApp");
app.UseAuthorization();

app.Run();
