using System.Linq;
using Athena.Backlog.UseCases;

namespace Athena.Backlog.Adapters;

public class BacklogControllerAdapter
{
    private BacklogInteractor myInteractor;

    public BacklogControllerAdapter(BacklogInteractor interactor)
    {
        myInteractor = interactor;
    }

    public BacklogVM GetBacklog(string teamName)
    {
        // TODO: fetch the team by name
        var team = new Team { Name = teamName };

        var response = myInteractor.GetBacklog(new BacklogRequestModel
        {
            Team = team
        });

        return new BacklogVM
        {
            WorkItems = response.WorkItems
                .Select(x => new WorkItemVM
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    AssignedTo = x.AssignedTo.Name,
                    State = x.State.ToString()
                })
                .ToList(),
            TotalEffort = string.Format("{0:0.00}", response.TotalEffort),
            TotalCapacity = string.Format("{0:0.00}", response.TotalCapacity)
        };
    }
}
