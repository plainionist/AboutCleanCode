using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Orchestrator.Tests")]

namespace AboutCleanCode.Orchestrator
{
    internal abstract class AbstractAgent : IAgent
    {
        private readonly Channel<Envelope> myQueue;

        internal AbstractAgent(ILogger logger)
        {
            Logger = logger;
            myQueue = Channel.CreateUnbounded<Envelope>();
        }

        protected ILogger Logger { get; }

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

            Logger.Info(this, "started");
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

            Logger.Info(this, "stopped");
        }
    }
}