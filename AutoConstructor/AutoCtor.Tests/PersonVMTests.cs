namespace AutoCtor.Tests;

public class PersonVMTests
{
    [Test]
    public void ConstructorGenerated()
    {
        var person = new PersonVM("John Doe", "Created");

        Assert.That(person.Name, Is.EqualTo("John Doe"));
        Assert.That(person.Status, Is.EqualTo("Created"));
    }

    [Test]
    public void ConstructorContainsGuards()
    {
        Assert.Throws<ArgumentNullException>(() => new PersonVM("John Doe", null));
    }
}

