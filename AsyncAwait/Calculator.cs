using System;
using System.Threading;

namespace AsyncAwait;

public record Request(int Value1, int Value2);
public record Response(int Result);

public class Calculator
{
    public Promise Compute(Request request)
    {
        Console.WriteLine("Starting computation ...");

        var promise = new Promise();

        new Thread(() =>
        {
            try
            {
                var result = SimulateExpensiveComputation(request);
                promise.SetResult(new Response(result));
            }
            catch (Exception ex)
            {
                promise.SetException(ex);
            }
        }).Start();

        Console.WriteLine("Returning to caller");

        return promise;
    }

    private static int SimulateExpensiveComputation(Request request)
    {
        for (int i = 0; i < 5; ++i)
        {
            Console.Write(".");
            Thread.Sleep(500);
        }
        Console.WriteLine(" finished!");

        return request.Value1 + request.Value2;
    }
}
