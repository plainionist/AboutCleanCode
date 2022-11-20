namespace EventGuards;

internal class Caller
{
    public void Initialize(SomeModel model)
    {
        // TODO: temporarily disable events ?

        model.Value1 = "foo";
        model.Value2 = "bar";
        model.Value3 = "42";

        SetFromConfiguration(model);
    }

    public void SetFromConfiguration(SomeModel model)
    {
        // TODO: temporarily disable events ?

        model.Value2 = GetConfigurationValue("Value2", model.Value2);
        model.Value3 = GetConfigurationValue("Value3", model.Value3);
    }

    private string GetConfigurationValue(string name, string defaultValue) =>
        // TODO: implement
        null;
}
