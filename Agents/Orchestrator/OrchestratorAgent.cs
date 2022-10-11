using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("Orchestrator.Tests")]

namespace AboutCleanCode.Orchestrator
{
    internal class OrchestratorAgent : AbstractAgent
    {
        private readonly ILogger myLogger;
        private readonly IAgent myDataCollectorTask;
        private readonly IDictionary<Guid, Job> myActiveJobs;

        internal OrchestratorAgent(ILogger logger, IAgent dataCollectorTask)
            : base(logger)
        {
            myLogger = logger;
            myDataCollectorTask = dataCollectorTask;
            myActiveJobs = new Dictionary<Guid, Job>();
        }

        /// <summary>
        /// Test API
        /// </summary>
        public IAgent StateObserver { get; set; }

        private void OnJobRequestReceived(string request)
        {
            var job = ParseRequest(request);
            myActiveJobs[job.Id] = job;

            // trigger collection of all relevant data before the data
            // of the job can be processed
            myDataCollectorTask.Post(this, new CollectDataCommand { JobId = job.Id });
        }

        private Job ParseRequest(string request)
        {
            var requestXml = XElement.Parse(request);
            return new Job(new Guid(requestXml.Element("Id").Value));
        }

        protected override void OnReceive(IAgent sender, object message)
        {
            if (message is JobRequestReceivedMessage jrrm)
            {
                OnJobRequestReceived(jrrm.Content);
            }
            else if (message is TaskStartedEvent tse)
            {
                OnDataCollectionStarted(tse);
            }
            else if (message is TaskCompletedEvent tce)
            {
                OnDataCollectionCompleted(tce);
            }
            else if (message is TaskFailedEvent tfe)
            {
                OnDataCollectionFailed(tfe);
            }
            else
            {
                throw new NotSupportedException(message.GetType().FullName);
            }
        }

        private void OnDataCollectionStarted(TaskStartedEvent e)
        {
            myLogger.Info(this, "OnDataCollectionStarted");

            myActiveJobs[e.JobId].Status = "DataCollectionStarted";
        }

        private void OnDataCollectionCompleted(TaskCompletedEvent e)
        {
            myLogger.Info(this, "OnDataCollectionCompleted");

            myActiveJobs[e.JobId].Status = "DataCollectionCompleted";

            // TODO: decide about next step and trigger its execution

            StateObserver?.Post(this, new JobStateChanged(e.JobId));
        }

        private void OnDataCollectionFailed(TaskFailedEvent e)
        {
            myLogger.Info(this, "OnDataCollectionFailed");

            myActiveJobs[e.JobId].Status = "DataCollectionFailed";

            // TODO: error handling - inform operators

            StateObserver?.Post(this, new JobStateChanged(e.JobId));
        }
    }
}