using System.Collections.Generic;
using Radar.Entities;

namespace Radar.SourceControl;

public interface ISourceControl
{
    Changeset Query(CodeBase codeBase, int changesetId);

    IReadOnlyCollection<Changeset> Query(CodeBase codeBase, VersionSpec fromVersion, VersionSpec toVersion);

    string GetContent(int changesetId, CodeBaseItem item);
}
