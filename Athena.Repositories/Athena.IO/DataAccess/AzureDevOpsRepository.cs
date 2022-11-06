using System;
using System.Collections.Generic;
using System.Linq;
using Athena.Adapters.DataAccess;
using Athena.Core.Domain;
using Athena.Core.UseCases;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;

namespace Athena.IO.DataAccess;

internal class AzureDevOpsRepository : ISqlDatabase
{
    private Uri myTpcUrl;
    private string[] myColumns;

    public IEnumerable<WorkItemDTO> Query(string wiql)
    {
        var credentials = new VssCredentials(new WindowsCredential(true));
        var client = new WorkItemTrackingHttpClient(myTpcUrl, credentials);

        var query = new Wiql();
        query.Query = wiql;

        var links = client.QueryByWiqlAsync(query).Result;

        var ids = links.WorkItems
            .Select(x => x.Id)
            .ToArray();

        var workItems = client.GetWorkItemsAsync(ids, myColumns).Result;

        return workItems
            .Select(x => new WorkItemDTO
            {
                Id = x.Id,
                Fields = x.Fields
            })
            .ToList();
    }

    public void Execute(string sql)
    {
        throw new NotImplementedException();
    }
}
