using System;
using System.Globalization;
using System.Linq;
using Athena.Backlog.UseCases;

namespace Athena.Backlog.Adapters;

public class BacklogControllerAdapter
{
    private readonly ITeamsRepository myTeamsRepository;
    private readonly BacklogInteractor myInteractor;

    public BacklogControllerAdapter(BacklogInteractor interactor, ITeamsRepository teamsRepository)
    {
        myInteractor = interactor;
        myTeamsRepository = teamsRepository;
    }

    public BacklogVM GetBacklog(string name, string iteration)
    {
        var team = myTeamsRepository.TryFindByName(name);
        if (team == null)
        {
            throw new NotFoundException($"Team not found: {name}");
        }

        if (!TryParseDateTime(iteration, out var iterationStart))
        {
            throw new BadRequestException("Invalid date/time format");
        }

        var response = myInteractor.GetBacklog(new BacklogRequestModel
        {
            Team = team,
            IterationStart = iterationStart
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

    private static bool TryParseDateTime(string value, out DateTime? result)
    {
        result = null;

        // optional parameters are not treated as error
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        if (DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out var date))
        {
            result = date;
            return true;
        }
        else
        {
            return false;
        }
    }
}
