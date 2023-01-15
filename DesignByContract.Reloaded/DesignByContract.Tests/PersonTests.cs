using System;
using NUnit.Framework;

namespace DesignByContract.Tests;

public class PersonTests
{
    [Test]
    public void Ctor_AddressNull_Throws()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new Person("John", "Doe", null));
        Assert.That(ex.Message, Contains.Substring("'address'"));
    }

    [Test]
    public void Ctor_LastNameTooLong_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new Person("John", "Dummy Last Name, long enough to make the constraints fail", new Address( /* TBD */ )));
        Assert.That(ex.Message, Contains.Substring("lastname").IgnoreCase);
    }
}