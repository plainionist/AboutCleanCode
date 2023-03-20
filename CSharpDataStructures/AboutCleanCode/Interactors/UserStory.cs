namespace AboutCleanCode.Interactors;

class UserStory
{
    private string myTitle;

    public UserStory(int id, string title)
    {
        Contract.Requires(id >= 0, "Id must not be negative");

        Id = id;
        Title = title;
    }

    public int Id { get; }

    public string Title
    {
        get { return myTitle; }
        set
        {
            Contract.RequiresNotNullNotEmpty(value);
            myTitle = value;
        }
    }

    /// <summary>
    /// Description is optional, hence null and empty string are allowed
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// UserStories can be unassigned, hence null is explicitly allowed.
    /// </summary>
    public Developer AssignedTo { get; set; }

    public double RemainingWork { get; private set; }
    public double CompletedWork { get; private set; }

    public void UpdateEfforts(double remainingWork, double completedWork)
    {
        Contract.Requires(remainingWork >= 0, "RemainingWork must not be negative");
        Contract.Requires(completedWork >= 0, "RemainingWork must not be negative");

        RemainingWork = remainingWork;
        CompletedWork = completedWork;
    }
}