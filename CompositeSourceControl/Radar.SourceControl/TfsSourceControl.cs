using System;
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

    public Option<Changeset> Query(CodeBase codeBase, SourceControlId id)
    {
        return id
            .Select(x => Option.Some(x.Value), _ => Option.None<int>("Not a TFS changeset id"))
            .Select(id => CreateChangeset(myConnection.GetChangesetAsync(id).Result, codeBase));
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

        fromVersion.Match(
            id => criteria.FromId = id.Id.Select(x => x.Value, _ => 0),
            date => criteria.FromDate = date.Date.ToString("yyyy-MM-dd HH:mm:ss"));
        toVersion.Match(
            id => criteria.ToId = id.Id.Select(x => x.Value, _ => 0),
            date => criteria.ToDate = date.Date.ToString("yyyy-MM-dd HH:mm:ss"));

        if (criteria.FromId == 0 && criteria.FromDate == null)
        {
            // cannot handle input parameter
            return new List<Changeset>();
        }

        return myConnection.GetChangesetsAsync(project: "", searchCriteria: criteria, top: int.MaxValue).Result
            .AsParallel()
            .Select(x => CreateChangeset(x, codeBase))
            .ToList();
    }

    public Option<string> GetContent(SourceControlId id, CodeBaseItem item)
    {
        return id
            .Select(x => Option.Some(x.Value), _ => Option.None<int>("Not a valid TFS changeset id"))
            .Select(id => ReadContent(id));

        string ReadContent(int id)
        {
            var serverPath = $"$/project/{item.CodeBase}/" + item.RelativePath;

            var stream = myConnection.GetItemContentAsync(path: serverPath, versionDescriptor: new TfvcVersionDescriptor(null, TfvcVersionType.Changeset, id.ToString())).Result;
            using var streamReader = new StreamReader(stream);
            var content = streamReader.ReadToEnd();
            return content;
        }
    }
}
