namespace Viewer
{
    public class Filter
    {
    }
    public class ZoomFilter : Filter
    {
        public ZoomFilter(double v)
        {
            V = v;
        }

        public double V { get; }
    }
}