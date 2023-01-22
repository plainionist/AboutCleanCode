namespace WeatherApp;

public static class EnumerableExtensions
{
    public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return self.ToList();
    }
}
