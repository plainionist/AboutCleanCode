using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Radar.Entities;

namespace Radar.SourceControl;

class TfsSourceControl : ISourceControl
{
    private readonly TfvcHttpClient myConnection;

    public TfsSourceControl()
    {
        myConnection = CreateConnection();
    }

    private TfvcHttpClient CreateConnection()
    {
        // TODO: implement
        return null;
    }

    public Changeset Query(CodeBase codeBase, int changesetId)
    {
        var changeset = myConnection.GetChangesetAsync(changesetId).Result;
        return CreateChangeset(changeset, codeBase);
    }

    private Changeset CreateChangeset(TfvcChangesetRef arg, CodeBase codeBase)
    {
        // TODO: fetch changes of this changeset and convert into changeset entity
        return null;
    }

    public IReadOnlyCollection<Changeset> Query(CodeBase codeBase, VersionSpec fromVersion, VersionSpec toVersion)
    {
        Contract.RequiresNotNull(codeBase, nameof(codeBase));
        Contract.RequiresNotNull(fromVersion, nameof(fromVersion));
        Contract.RequiresNotNull(toVersion, nameof(toVersion));

        var criteria = new TfvcChangesetSearchCriteria
        {
            ItemPath = $"$/project/{codeBase}/Main"
        };

        fromVersion.Match(id => criteria.FromId = id.Id, date => criteria.FromDate = date.Date.ToString("yyyy-MM-dd HH:mm:ss"));
        toVersion.Match(id => criteria.ToId = id.Id, date => criteria.ToDate = date.Date.ToString("yyyy-MM-dd HH:mm:ss"));

        return myConnection.GetChangesetsAsync(project: "", searchCriteria: criteria, top: int.MaxValue).Result
            .AsParallel()
            .Select(x => CreateChangeset(x, codeBase))
            .ToList();
    }

    public string GetContent(int changesetId, CodeBaseItem item)
    {
        var serverPath = $"$/project/{item.CodeBase}/" + item.RelativePath;

        var stream = myConnection.GetItemContentAsync(path: serverPath, versionDescriptor: new TfvcVersionDescriptor(null, TfvcVersionType.Changeset, changesetId.ToString())).Result;
        using var streamReader = new StreamReader(stream);
        var content = streamReader.ReadToEnd();
        return content;
    }
}
