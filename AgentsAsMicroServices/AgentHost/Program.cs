
using AboutCleanCode.AgentHost;

var host = new HttpAgentsHost(new ConsoleLogger());
host.RunAsync(args);