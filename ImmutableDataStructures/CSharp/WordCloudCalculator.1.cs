
using System.Collections.Generic;
using System.Linq;

namespace MyApp
{
    public class AlternativeWordCloudCalculator
    {
        public IReadOnlyDictionary<string, int> Calculate(IEnumerable<Person> persons) =>
            persons
                .AsParallel()
                .SelectMany(x => new[] { x.FirstName, x.SecondName, x.LastName })
                .Where(x => x != null)
                .Select(x => x.ToLower())
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
    }
}