using WarningRadar;

var buildLog = args[0];
var config = Config.Load();

Console.WriteLine($"BuildLog: {buildLog}");

var parser = new LogParser();
var alerts = parser.Parse(new StreamReader(buildLog));

var linkBuilder = new GitHubLinkBuilder(config.BaseUrl, config.WorkspaceRoot);

var report = new HtmlReport(linkBuilder);
report.Generate(alerts, Console.Out);
