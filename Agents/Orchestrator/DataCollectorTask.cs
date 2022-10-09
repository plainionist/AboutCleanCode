using System;
using System.Threading.Tasks;

namespace AboutCleanCode.Orchestrator;

class DataCollectorTask 
{
    public event EventHandler<TaskStartedEventArgs> TaskStarted;
    public event EventHandler<TaskCompletedEventArgs> TaskCompleted;
    public event EventHandler<TaskFailedEventArgs> TaskFailed;

    public void Process(Guid jobId)
    {
        Task.Run(() => {
            try
            {
                TaskStarted?.Invoke(this, new TaskStartedEventArgs(jobId));

                // TODO: collect all necessary data which takes quite some time

                object payload = null; // TODO: carries the collected data

                TaskCompleted?.Invoke(this, new TaskCompletedEventArgs(jobId, payload));
            }
            catch (Exception exception)
            {
                TaskFailed?.Invoke(this, new TaskFailedEventArgs(jobId, exception));
            }
        });
    }
}