using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Radar.Entities;

namespace Radar.SourceControl;

class GitSourceControl : ISourceControl
{
    private readonly GitHttpClient myConnection;
    private readonly IRepositoryLookup myRepositoryLookup;

    public GitSourceControl(IRepositoryLookup repositoryLookup)
    {
        myRepositoryLookup = repositoryLookup;

        myConnection = CreateConnection();
    }

    private GitHttpClient CreateConnection()
    {
        // TODO: implement
        return null;
    }

    public Option<Changeset> Query(CodeBase codeBase, SourceControlId id)
    {
        return id
            .Select(_ => Option.None<string>("Not a valid Git commit id"), x => Option.Some(x.Value))
            .Select(QueryChangeset);

        Changeset QueryChangeset(string id)
        {
            var repo = myRepositoryLookup.GetRepository(codeBase);

            var changeset = myConnection.GetCommitAsync(project: "", id, repo.Id).Result;
            return CreateChangeset(changeset, codeBase);
        }
    }

    private Changeset CreateChangeset(GitCommitRef arg, CodeBase codeBase)
    {
        // TODO: fetch changes of this changeset and convert into changeset entity
        return null;
    }

    public IReadOnlyCollection<Changeset> Query(CodeBase codeBase, VersionSpec fromVersion, VersionSpec toVersion)
    {
        Contract.RequiresNotNull(codeBase, nameof(codeBase));
        Contract.RequiresNotNull(fromVersion, nameof(fromVersion));
        Contract.RequiresNotNull(toVersion, nameof(toVersion));

        var repo = myRepositoryLookup.GetRepository(codeBase);

        var criteria = new GitQueryCommitsCriteria
        {
            ItemVersion = new GitVersionDescriptor() { Version = "Main" }
        };

        fromVersion.Match(
            id => criteria.FromCommitId = id.Id.Select(_ => null, x => x.Value),
            date => criteria.FromDate = date.Date.ToString("yyyy-MM-dd HH:mm:ss"));
        toVersion.Match(
            id => criteria.ToCommitId = id.Id.Select(_ => null, x => x.Value),
            date => criteria.ToDate = date.Date.ToString("yyyy-MM-dd HH:mm:ss"));

        if (criteria.FromCommitId == null && criteria.FromDate == null)
        {
            // cannot handle input parameter
            return new List<Changeset>();
        }

        return myConnection.GetCommitsAsync(repositoryId: repo.Id, searchCriteria: criteria, top: int.MaxValue).Result
            .Select(commit => CreateChangeset(commit, codeBase)).ToList();
    }

    public Option<string> GetContent(SourceControlId id, CodeBaseItem item)
    {
        return id
            .Select(_ => Option.None<string>("Not a valid Git commit id"), x => Option.Some(x.Value))
            .Select(ReadContent);

        string ReadContent(string id)
        {
            var repo = myRepositoryLookup.GetRepository(item.CodeBase);

            var stream = myConnection.GetItemContentAsync(project: "", repo.Id, id, item.RelativePath).Result;
            using var streamReader = new StreamReader(stream);
            var content = streamReader.ReadToEnd();
            return content;
        }
    }
}
