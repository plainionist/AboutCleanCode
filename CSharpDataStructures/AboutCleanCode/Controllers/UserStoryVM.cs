namespace AboutCleanCode.Controllers;

public class UserStoryVM
{
    public required int Id { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required string AssignedTo { get; init; }

    public required string RemainingWork { get; init; }

    public required string CompletedWork { get; init; }

    public required string StackRank { get; init; }
}
