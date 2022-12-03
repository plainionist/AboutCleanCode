using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AwaitableEvents;

public static class ProcessExtensions
{
    /// <summary>
    /// await Process.Start(“Foo.exe”);
    /// </summary>
    public static TaskAwaiter<int> GetAwaiter(this Process process)
    {
        var tcs = new TaskCompletionSource<int>();

        if (process.HasExited)
        {
            tcs.TrySetResult(process.ExitCode);
        }
        else
        {
            void OnExited(object sender, EventArgs e)
            {
                process.Exited -= OnExited;
                tcs.TrySetResult(process.ExitCode);
            }

            process.Exited += OnExited;

            process.EnableRaisingEvents = true;
        }

        return tcs.Task.GetAwaiter();
    }
}
