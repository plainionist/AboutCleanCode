namespace Naming
{
    public interface FooFactory
    {
        public XYZ Create();
        public XYZ Create(string name);
        public XYZ Create(string name, string description);
    }

    public class XYZ {}
}