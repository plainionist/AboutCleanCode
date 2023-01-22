using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using WeatherApp.Forecast.UseCases;
using WeatherApp.Mediator;
using WeatherApp.Mediator.MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(ForecastRequest));

builder.Services.AddSingleton<IApplicationMediator, MediatRAdapter>();

builder.Services.AddTransient(typeof(IRequestHandler<,>), typeof(RequestHandlerAdapter<,>));
builder.Services.AddTransient(typeof(INotificationHandler<>), typeof(NotificationHandlerAdapter<>));

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    var openTypes = new[]
    {
        typeof(IApplicationRequestHandler<,>),
        typeof(IApplicationNotificationHandler<>),
    };

    foreach (var openType in openTypes)
    {
        builder
            .RegisterAssemblyTypes(typeof(ForecastRequest).Assembly)
            .AsClosedTypesOf(openType)
            .AsImplementedInterfaces();
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
