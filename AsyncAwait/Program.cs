using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait;

public static class Program
{
    public static async Task Main()
    {
        var component = new Calculator();

        Console.WriteLine("Calling Component.Execute ...");

        var response = await component.Compute(new Request(17, 25));

        Console.WriteLine($"Result: {response.Result}");
    }
}