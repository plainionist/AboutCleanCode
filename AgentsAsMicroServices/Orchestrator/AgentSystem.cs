using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace AboutCleanCode.Orchestrator;

public interface IAgentSystem
{
    IAgent Select(string name);
    IAgent TrySelect(string name);
}

public class AgentSystem : IAgentSystem
{
    private Dictionary<string, IHostedAgent> myAgents = new();

    public IAgent Select(string name) =>
        myAgents[name];

    public IAgent TrySelect(string name) =>
        myAgents.TryGetValue(name, out var agent) ? agent : null;

    public void Register(IHostedAgent agent)
    {
        myAgents.Add(agent.Name, agent);
    }

    public void Register(string name, Uri baseUrl)
    {
        var clientProxy = new AgentClientProxy(name, baseUrl);
        myAgents.Add(name, clientProxy);
    }

    public void Start()
    {
        foreach (var agent in myAgents.Values)
        {
            agent.Start();
        }
    }

    public void Stop()
    {
        foreach (var agent in myAgents.Values)
        {
            agent.Stop();
        }
    }
}

public class AgentMessage
{
    public string Sender { get; set; }
    public string Message { get; set; }
}

class AgentClientProxy : IHostedAgent
{
    private readonly string myBaseUrl;

    public AgentClientProxy(string name, Uri baseUrl)
    {
        Name = name;
        myBaseUrl = baseUrl.ToString().TrimEnd('/');
    }

    public string Name { get; }

    public async void Post(IAgent sender, object message)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        var json = JsonConvert.SerializeObject(message, settings);

        var agentMessage = new AgentMessage
        {
            Sender = sender.Name,
            Message = json
        };

        using (var client = new HttpClient())
        {
            var response = await client.PostAsync(new Uri($"{myBaseUrl}{Name}"),
                JsonContent.Create(agentMessage));
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Failed to send message {message.GetType().Name} to {Name}");
            }
        }
    }

    public void Start()
    {
        // nothing to do
    }

    public void Stop()
    {
        // nothing to do
    }
}

public class AgentServerProxy : IAgent
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
