namespace AboutCleanCode.Orchestrator;

public interface IAgent
{
    void Post(IAgent sender, object message);
}
