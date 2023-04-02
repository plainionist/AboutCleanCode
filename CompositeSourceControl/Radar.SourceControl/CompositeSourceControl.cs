using System.Collections.Generic;
using System.Linq;
using Radar.Entities;

namespace Radar.SourceControl;

public class CompositeSourceControl : ISourceControl
{
    private readonly IReadOnlyCollection<ISourceControl> mySourceControls;

    public CompositeSourceControl(IReadOnlyCollection<ISourceControl> sourceControls)
    {
        Contract.RequiresNotNullNotEmpty(sourceControls, nameof(sourceControls));

        mySourceControls = sourceControls;
    }

    public Option<string> GetContent(SourceControlId id, CodeBaseItem item)
    {
        return mySourceControls
            .Select(x => x.GetContent(id, item))
            .First(x => x.IsSome);
    }

    public Option<Changeset> Query(CodeBase codeBase, SourceControlId id)
    {
        return mySourceControls
            .Select(x => x.Query(codeBase, id))
            .First(x => x.IsSome);
    }

    public IReadOnlyCollection<Changeset> Query(CodeBase codeBase, VersionSpec fromVersion, VersionSpec toVersion)
    {
        return mySourceControls
            .SelectMany(x => x.Query(codeBase, fromVersion, toVersion))
            .ToList();
    }
}
