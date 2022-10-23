using System.Collections.Generic;
using Newtonsoft.Json;

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
        var serverProxy = new AgentServerProxy(agent);
        // TODO: intermediate step - AgentClientProxy will later implement the real remoting
        var clientProxy = new AgentClientProxy(serverProxy);
        myAgents.Add(agent.Name, clientProxy);
    }
}

class AgentClientProxy : IAgent
{
    private readonly IAgent myImpl;

    public AgentClientProxy(IAgent impl)
    {
        myImpl = impl;
    }

    public string Name => $"/system/clientProxyOf({myImpl.Name})";

    public void Post(IAgent sender, object message)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        var json = JsonConvert.SerializeObject(message, settings);
        myImpl.Post(sender, json);
    }
}

class AgentServerProxy : IAgent
{
    private readonly IAgent myImpl;

    public AgentServerProxy(IAgent impl)
    {
        myImpl = impl;
    }

    public string Name => $"/system/serverProxyOf({myImpl.Name})";

    public void Post(IAgent sender, object message)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        var messageObj = JsonConvert.DeserializeObject(message.ToString(), settings);
        myImpl.Post(sender, messageObj);
    }
}
