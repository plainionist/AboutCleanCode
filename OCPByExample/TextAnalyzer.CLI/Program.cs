using System;
using System.IO;
using System.Linq;

namespace TextAnalyzer.CLI;

public class Program
{
    public static int Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Filename missing!");
            return -1;
        }

        var text = File.ReadAllText(args[0]);

        var analyzer = new WordsAnalyzer();
        var counts = analyzer.CountWords(text);

        foreach (var entry in counts.OrderBy(x => x.Value))
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }

        return 0;
    }
}
