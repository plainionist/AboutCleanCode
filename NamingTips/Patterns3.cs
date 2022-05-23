namespace Naming
{
    public interface IFooReader
    {
        public bool CanRead(int id);
        public string Read(int id);
    }
}