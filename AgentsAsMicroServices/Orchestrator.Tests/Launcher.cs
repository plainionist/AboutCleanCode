using System;
using System.Diagnostics;
using System.IO;

namespace AboutCleanCode.Orchestrator.Tests;

class Launcher
{
    private readonly FakeLogger myLogger;
    private Process myProcess;

    public Launcher(FakeLogger logger)
    {
        myLogger = logger;
    }

    public void Start(string name)
    {
        myProcess = new Process();
        myProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
        myProcess.StartInfo.FileName = "AgentHost.exe";
        myProcess.StartInfo.Arguments = $"--name={name}";
        myProcess.StartInfo.UseShellExecute = false;
        myProcess.StartInfo.RedirectStandardOutput = true;
        myProcess.StartInfo.RedirectStandardError = true;
        myProcess.StartInfo.CreateNoWindow = true;
        myProcess.ErrorDataReceived += OnOutputReceived;
        myProcess.OutputDataReceived += OnOutputReceived;
        myProcess.EnableRaisingEvents = true;
        myProcess.Start();
        myProcess.BeginErrorReadLine();
        myProcess.BeginOutputReadLine();
    }

    private void OnOutputReceived(object sender, DataReceivedEventArgs e)
    {
        myLogger.Messages.Enqueue(e.Data);
    }

    public void Stop()
    {
        myProcess.ErrorDataReceived -= OnOutputReceived;
        myProcess.OutputDataReceived -= OnOutputReceived;
        myProcess.Kill();
        myProcess.Dispose();
    }
}
