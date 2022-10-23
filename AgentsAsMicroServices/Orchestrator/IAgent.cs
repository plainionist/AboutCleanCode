namespace AboutCleanCode.Orchestrator;

public interface IAgent
{
    string Name { get; }
    void Post(IAgent sender, object message);
}
