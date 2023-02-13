namespace Athena.Backlog.UseCases;

public interface ITeamsRepository
{
    Team TryFindByName(string name);
}
