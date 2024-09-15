namespace AutoCtor.Tests;

public class PersonTests
{
    [Test]
    public void Ctor_WithValidArguments_PropertiesShouldBeInitialized()
    {
        var person = new Person("John", "Doe");

        Assert.That(person.FirstName, Is.EqualTo("John"));
        Assert.That(person.LastName, Is.EqualTo("Doe"));
    }

    [Test]
    public void Ctor_WithNull_ShouldThrow()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new Person("John", null));
        Assert.That(ex.Message, Contains.Substring("lastName"));
    }
}