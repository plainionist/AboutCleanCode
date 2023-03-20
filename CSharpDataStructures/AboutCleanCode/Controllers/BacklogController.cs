using System.Collections.Generic;
using System.Linq;
using AboutCleanCode.Interactors;

namespace AboutCleanCode.Controllers;

internal class BacklogController
{
    private readonly BacklogInteractor myInteractor;

    public BacklogController(BacklogInteractor interactor) =>
        myInteractor = interactor;

    public IReadOnlyCollection<UserStoryVM> Backlog() =>
        myInteractor.Backlog()
            .Select(x => new UserStoryVM
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                AssignedTo = x.AssignedTo.Name + " " + x.AssignedTo.LastName,
                RemainingWork = x.RemainingWork.ToString("0.00"),
                CompletedWork = x.CompletedWork.ToString("0.00"),
                StackRank = null
            })
            .ToList();
}
