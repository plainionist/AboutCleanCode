using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Orchestrator.Tests")]

namespace AboutCleanCode.Orchestrator
{
    internal abstract class AbstractAgent : IAgent
    {
        private readonly Channel<Envelope> myQueue;
        private readonly Dictionary<Type, Delegate> myMessageHandlers;

        internal AbstractAgent(ILogger logger)
        {
            Logger = logger;

            myQueue = Channel.CreateUnbounded<Envelope>();
            myMessageHandlers = new Dictionary<Type, Delegate>();
        }

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
            _ = Listen();

            Logger.Info(this, "started");
        }

        private async Task Listen()
        {
            await foreach (var envelope in myQueue.Reader.ReadAllAsync())
            {
                if (envelope.Message is PoisonPill)
                {
                    Logger.Info(this, "stopped");
                    break;
                }

                try
                {
                    OnReceive(envelope.Sender, envelope.Message);
                }
                catch (Exception ex)
                {
                    Logger.Error(this, $"Failed to process '{envelope.Message.GetType().FullName}' from " +
                        $"'{envelope.Sender.GetType().FullName}': {Environment.NewLine}{ex}");
                }
            }
        }

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

            // TODO: wait for task to be completed
        }
    }
}