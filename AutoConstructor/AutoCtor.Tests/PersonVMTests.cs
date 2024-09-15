namespace AutoCtor.Tests;

public class PersonVMTests
{
    [Test]
    public void InitPropertiesNotEnforcedByCtor()
    {
        var person = new PersonVM("John Doe");
        person.Status = "Created";

        Assert.That(person.Name, Is.EqualTo("John Doe"));
        Assert.That(person.Status, Is.EqualTo("Created"));
    }
}