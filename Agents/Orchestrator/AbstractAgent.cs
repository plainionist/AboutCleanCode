using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("Orchestrator.Tests")]

namespace AboutCleanCode.Orchestrator
{
    internal abstract class AbstractAgent : IAgent
    {
        private readonly ILogger myLogger;
        private readonly Channel<Envelope> myQueue;

        internal AbstractAgent(ILogger logger)
        {
            myLogger = logger;
            myQueue = Channel.CreateUnbounded<Envelope>();
        }

        public void Post(IAgent sender, object message)
        {
            myQueue.Writer.WriteAsync(new Envelope
            {
                Sender = sender,
                Messages = message
            });
        }

        private class Envelope
        {
            public IAgent Sender { get; init; }
            public object Messages { get; init; }
        }

        public void Start()
        {
            _ = Listen();

            myLogger.Info(this, "started");
        }

        private async Task Listen()
        {
            await foreach( var envelope in myQueue.Reader.ReadAllAsync())
            {
                if (envelope.Messages is PoisonPill)
                {
                    break;
                }
                else
                {
                    OnReceive(envelope.Sender, envelope.Messages);
                }
            }
        }

        protected abstract void OnReceive(IAgent sender, object message);

        public void Stop()
        {
            Post(this, new PoisonPill());

            // TODO: wait for task to be completed

            myLogger.Info(this, "stopped");
        }
    }
}