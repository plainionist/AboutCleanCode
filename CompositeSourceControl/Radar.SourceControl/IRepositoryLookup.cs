using Radar.Entities;

namespace Radar.SourceControl;

public interface IRepositoryLookup
{
    Repository GetRepository(CodeBase codeBase);
    CodeBase GetCodeBase(Repository repository);
}

public sealed class Repository
{
    public Repository(string id, CodeBase codeBase)
    {
        Contract.RequiresNotNull(id, nameof(id));
        Contract.RequiresNotNull(codeBase, nameof(codeBase));

        Id = id;
        CodeBase = codeBase;
    }

    public string Id { get; }
    public CodeBase CodeBase { get; }
}

