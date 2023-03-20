namespace AboutCleanCode.Interactors;

public record Developer(string Name, string LastName, EMailAddress EMail)
{
    private readonly bool IsValid = RecordValidator.Verify
        .NotNullNotEmpty(Name)
        .NotNullNotEmpty(LastName)
        .NotNull(EMail)
        .IsValid();
}
