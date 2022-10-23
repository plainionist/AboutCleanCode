using System.Threading.Tasks;
using AboutCleanCode.Orchestrator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace AboutCleanCode.AgentHost;

public class HttpAgentsHost
{
    private readonly ILogger myLogger;
    private WebApplication myApp;

    public HttpAgentsHost(ILogger logger)
    {
        myLogger = logger;
    }

    public AgentSystem AgentSystem{get;private set;}

    public Task RunAsync(params string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var name = builder.Configuration.GetSection("name").Value;
        var cfg = SystemConfiguration.Build(name, myLogger);
        AgentSystem = cfg.AgentSystem;

        myApp = builder.Build();
        myApp.Urls.Clear();
        myApp.Urls.Add(cfg.BaseUrl);

        myApp.MapPost("/user/{**agent}", (string agent, [FromBody] AgentMessage request) =>
            new AgentServerProxy(AgentSystem.Select($"/user/{agent}"))
                .Post(AgentSystem.Select(request.Sender), request.Message));

        AgentSystem.Start();

        return myApp.RunAsync();
    }

    public Task Stop()
    {
        AgentSystem.Stop();
        return myApp.StopAsync();
    }
}
