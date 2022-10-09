using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("Orchestrator.Tests")]

namespace AboutCleanCode.Orchestrator
{
    internal class Orchestrator
    {
        private readonly ILogger myLogger;
        private readonly DataCollectorTask myDataCollectorTask;
        private readonly IDictionary<Guid, Job> myActiveJobs;

        internal Orchestrator(ILogger logger, DataCollectorTask dataCollectorTask)
        {
            myLogger = logger;
            myDataCollectorTask = dataCollectorTask;
            myActiveJobs = new Dictionary<Guid, Job>();
        }

        public void ProcessJobRequest(string request)
        {
            var job = ParseRequest(request);
            myActiveJobs[job.Id] = job;

            // trigger collection of all relevant data before the data
            // of the job can be processed
            myDataCollectorTask.Process(job.Id);
        }

        private Job ParseRequest(string request)
        {
            var requestXml = XElement.Parse(request);
            return new Job(new Guid(requestXml.Element("Id").Value));
        }

        public void Start()
        {
            myDataCollectorTask.TaskStarted += OnDataCollectionStarted;
            myDataCollectorTask.TaskCompleted += OnDataCollectionCompleted;
            myDataCollectorTask.TaskFailed += OnDataCollectionFailed;

            myLogger.Info(this, "started");
        }

        private void OnDataCollectionStarted(object sender, TaskStartedEventArgs e)
        {
            myLogger.Info(this, "OnDataCollectionStarted");
            
            myActiveJobs[e.JobId].Status = "DataCollectionStarted";
        }

        private void OnDataCollectionCompleted(object sender, TaskCompletedEventArgs e)
        {
            myLogger.Info(this, "OnDataCollectionCompleted");

            myActiveJobs[e.JobId].Status = "DataCollectionCompleted";

            // TODO: decide about next step and trigger its execution
        }

        private void OnDataCollectionFailed(object sender, TaskFailedEventArgs e)
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

        public void Stop()
        {
            myDataCollectorTask.TaskStarted -= OnDataCollectionStarted;
            myDataCollectorTask.TaskCompleted -= OnDataCollectionCompleted;
            myDataCollectorTask.TaskFailed -= OnDataCollectionFailed;

            myLogger.Info(this, "stopped");
        }
    }
}