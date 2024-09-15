
namespace AutoCtor;

public interface ISqlConnectionFactory
{
    IDisposable Create();
}
