using System.Text.RegularExpressions;

namespace AboutCleanCode.GraphFundamentals.Simple;


record LoggedCallId(string Service, string CallId, bool IsBegin);

class CallIdAnalyzer
{
    // 2025-02-14 15:56:48.002295 18240/26832 Product.ServiceA -> BEGIN-CALL: 5afb3e4e-691a-4b7d-8119-8f42e8843455
    private IEnumerable<LoggedCallId> ParseFile(string file)
    {
        foreach (var line in File.ReadAllLines(file))
        {
            var parts = line.Split(' ');
            if (parts.Length < 7) continue;

            bool? isBegin = parts[5] switch
            {
                "BEGIN-CALL:" => true,
                "END-CALL:" => false,
                _ => null
            };
            if (isBegin == null) continue;

            yield return new LoggedCallId(parts[3], parts[6], isBegin.Value);
        }
    }

    public void Analyze(string file)
    {
        var calls = ParseFile(file);

        var edges = calls
            // Each service-to-service call has a unique call ID
            .GroupBy(c => c.CallId)
            .Select(c => new Edge(c.ElementAt(0).Service, c.ElementAt(1).Service))
            .ToList();

        FindAllPaths(edges, "Product.ServiceA", "Product.ServiceB", []);
    }

    // Attention: only works with DAGs
    static void FindAllPaths(IEnumerable<Edge> edges, string current, string target, List<string> path)
    {
        path.Add(current);

        if (current == target)
            Console.WriteLine(string.Join(" -> ", path));
        else
            foreach (var edge in edges)
                if (edge.Source == current)
                    FindAllPaths(edges, edge.Target, target, path.ToList());
    }
}

record Edge(string Source, string Target);