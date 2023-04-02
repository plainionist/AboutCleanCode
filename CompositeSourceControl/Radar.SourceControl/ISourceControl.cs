using System.Collections.Generic;
using Radar.Entities;

namespace Radar.SourceControl;

public interface ISourceControl
{
    Option<Changeset> Query(CodeBase codeBase, SourceControlId id);

    IReadOnlyCollection<Changeset> Query(CodeBase codeBase, VersionSpec fromVersion, VersionSpec toVersion);

    Option<string> GetContent(SourceControlId id, CodeBaseItem item);
}
