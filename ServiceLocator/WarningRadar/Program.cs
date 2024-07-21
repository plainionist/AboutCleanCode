using WarningRadar;

var msbuildOutput = args[0];
Console.WriteLine($"Analyzing {msbuildOutput}");

var parser = new LogParser();
var alerts = parser.Parse(new StreamReader(msbuildOutput));

var report = new HtmlReport(Console.Out);
report.Generate(alerts);

