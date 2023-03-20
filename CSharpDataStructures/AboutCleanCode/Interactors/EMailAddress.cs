namespace AboutCleanCode.Interactors;

public record EMailAddress(string Value)
{
    private readonly bool IsValid = RecordValidator.Verify.NotNull(Value).IsValid();
}
