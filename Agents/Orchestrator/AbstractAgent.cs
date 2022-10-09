using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("Orchestrator.Tests")]

namespace AboutCleanCode.Orchestrator
{
    internal abstract class AbstractAgent : IAgent
    {
        private readonly ILogger myLogger;
        private readonly BlockingCollection<Envelope> myQueue;
        private Thread myThread;

        internal AbstractAgent(ILogger logger)
        {
            myLogger = logger;
            myQueue = new BlockingCollection<Envelope>();
        }

        public void Post(IAgent sender, object message)
        {
            myQueue.Add(new Envelope
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
            myThread = new Thread(Body);
            myThread.Start();

            myLogger.Info(this, "started");
        }

        private void Body()
        {
            while (true)
            {
                var envelope = myQueue.Take();

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

            myThread.Join();
            myThread = null;

            myLogger.Info(this, "stopped");
        }
    }
}