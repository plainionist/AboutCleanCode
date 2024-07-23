﻿using WarningRadar;

var buildLog = args[0];

Console.WriteLine($"BuildLog: {buildLog}");

var parser = new LogParser();
var alerts = parser.Parse(new StreamReader(buildLog));

var report = new HtmlReport();
report.Generate(alerts, Console.Out);
