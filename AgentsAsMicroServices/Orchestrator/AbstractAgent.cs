using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AboutCleanCode.Orchestrator;

public interface IHostedAgent : IAgent
{
    void Start();
    void Stop();
}

internal abstract class AbstractAgent : IHostedAgent
{
    private readonly Channel<Envelope> myQueue;
    private readonly Dictionary<Type, Delegate> myMessageHandlers;
    private Task myListeningTask;
    private readonly IAgent mySupervisor;

    internal AbstractAgent(ILogger logger, string name)
        : this(logger, name, null)
    { }

    internal AbstractAgent(ILogger logger, string name, IAgent supervisor)
    {
        Logger = logger;
        Name = name;
        mySupervisor = supervisor;

        myQueue = Channel.CreateUnbounded<Envelope>();
        myMessageHandlers = new Dictionary<Type, Delegate>();
    }

    public string Name { get; }

    protected void Receive<T>(Action<IAgent, T> handler)
    {
        if (myMessageHandlers.ContainsKey(typeof(T)))
        {
            throw new ArgumentException($"Handler already registered for messages of type: {typeof(T)}");
        }

        myMessageHandlers[typeof(T)] = handler;
    }

    protected ILogger Logger { get; }

    public void Post(IAgent sender, object message)
    {
        myQueue.Writer.WriteAsync(new Envelope
        {
            Sender = sender,
            Message = message
        });
    }

    private class Envelope
    {
        public IAgent Sender { get; init; }
        public object Message { get; init; }
    }

    public void Start()
    {
        myListeningTask = Listen();

        Logger.Info(this, "started");

        PostStart();
    }

    private async Task Listen()
    {
        await foreach (var envelope in myQueue.Reader.ReadAllAsync())
        {
            if (envelope.Message is PoisonPill)
            {
                PreStop();

                Logger.Info(this, "stopped");

                break;
            }

            try
            {
                OnReceive(envelope.Sender, envelope.Message);
            }
            catch (Exception ex)
            {
                if (mySupervisor != null)
                {
                    mySupervisor.Post(this, new MessageProcessingFailedEvent(envelope.Message, ex));
                }
                else
                {
                    Logger.Error(this, $"Failed to process '{envelope.Message.GetType().FullName}' from " +
                        $"'{envelope.Sender.GetType().FullName}': {Environment.NewLine}{ex}");
                }
            }
        }
    }

    protected virtual void PreStop() { }
    protected virtual void PostStart() { }

    protected virtual void OnReceive(IAgent sender, object message)
    {
        if (myMessageHandlers.TryGetValue(message.GetType(), out var handler))
        {
            handler.DynamicInvoke(sender, message);
        }
        else
        {
            Logger.Warning(this, $"Unexpected message: {message.GetType()}");
        }
    }

    public void Stop()
    {
        Post(this, new PoisonPill());

        myListeningTask.Wait();
    }
}