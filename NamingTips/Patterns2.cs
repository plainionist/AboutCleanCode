
namespace Naming
{
    public interface FooRepository
    {
        public void Add(Document doc);
        public void Remove(Document doc);
        public XYZ FindByName(string name);
    }

    public class Document {}
}