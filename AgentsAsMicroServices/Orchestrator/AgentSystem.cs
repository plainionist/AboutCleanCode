using System.Collections.Generic;

namespace AboutCleanCode.Orchestrator;

public interface IAgentSystem
{
    IAgent Select(string name);
}

public class AgentSystem : IAgentSystem
{
    private Dictionary<string, IAgent> myAgents = new();

    public IAgent Select(string name) =>
        myAgents[name];

    public void Register(IAgent agent)
    {
        myAgents.Add(agent.Name, agent);
    }
}

