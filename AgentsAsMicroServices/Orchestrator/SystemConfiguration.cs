using System;

namespace AboutCleanCode.Orchestrator;

public class SystemConfiguration
{
    public string BaseUrl { get; private set; }
    public AgentSystem AgentSystem { get; private set; }

    public static SystemConfiguration Build(string name, ILogger logger) =>
        name switch
        {
            "Orchestrator" => BuildOrchestrator(logger),
            "DataCollector" => BuildDataCollector(logger),
            _ => BuildLocal()
        };

    private static SystemConfiguration BuildOrchestrator(ILogger logger)
    {
        var cfg = new SystemConfiguration();
        cfg.BaseUrl = "http://localhost:7777/";
        cfg.AgentSystem = new AgentSystem();
        cfg.AgentSystem.Register(new OrchestratorAgent(logger, cfg.AgentSystem));
        cfg.AgentSystem.Register("/user/dataCollector", new Uri("http://localhost:8888/"));
        cfg.AgentSystem.Register("/user/tests/jobObserver", new Uri("http://localhost:9999/"));
        return cfg;
    }

    private static SystemConfiguration BuildDataCollector(ILogger logger)
    {
        var cfg = new SystemConfiguration();
        cfg.BaseUrl = "http://localhost:8888/";
        cfg.AgentSystem = new AgentSystem();
        cfg.AgentSystem.Register(new DataCollectorAgent(logger));
        cfg.AgentSystem.Register("/user/orchestrator", new Uri("http://localhost:7777/"));
        cfg.AgentSystem.Register("/user/tests/jobObserver", new Uri("http://localhost:9999/"));
        return cfg;
    }

    private static SystemConfiguration BuildLocal()
    {
        var cfg = new SystemConfiguration();
        cfg.BaseUrl = "http://localhost:9999/";
        cfg.AgentSystem = new AgentSystem();
        cfg.AgentSystem.Register("/user/orchestrator", new Uri("http://localhost:7777/"));
        cfg.AgentSystem.Register("/user/dataCollector", new Uri("http://localhost:8888/"));
        return cfg;
    }
}
