using Athena.Backlog.UseCases;

namespace Athena.Backlog.Adapters;

public class BacklogController
{
    private BacklogInteractor myInteractor;

    public BacklogController(BacklogInteractor interactor)
    {
        myInteractor = interactor;
    }

    public BacklogVM GetBacklog(string team)
    {
        // TODO: fetch the team by name
        // TODO: get the backlog from interactor
        // TODO: convert response model into response object by formatting all data
        return new BacklogVM
        {

        };
    }
}
