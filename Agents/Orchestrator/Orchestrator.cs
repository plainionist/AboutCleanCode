using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("Orchestrator.Tests")]

namespace AboutCleanCode.Orchestrator
{
    internal class Orchestrator : AbstractAgent
    {
        private readonly ILogger myLogger;
        private readonly IAgent myDataCollectorTask;
        private readonly IDictionary<Guid, Job> myActiveJobs;

        internal Orchestrator(ILogger logger, IAgent dataCollectorTask)
            : base(logger)
        {
            myLogger = logger;
            myDataCollectorTask = dataCollectorTask;
            myActiveJobs = new Dictionary<Guid, Job>();
        }

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
        }

        private void OnDataCollectionFailed(TaskFailedEvent e)
        {
            myLogger.Info(this, "OnDataCollectionFailed");

            myActiveJobs[e.JobId].Status = "DataCollectionFailed";

            // TODO: error handling - inform operators
        }

        /// <summary>
        /// Test Api
        /// </summary>
        public string GetJobStatus(Guid jobId) =>
            myActiveJobs.TryGetValue(jobId, out var job) ? job.Status : "<unknown>";
    }
}