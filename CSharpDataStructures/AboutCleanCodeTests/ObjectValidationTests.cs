using System;
using NUnit.Framework;
using AboutCleanCode.Interactors;

namespace AboutCleanCodeTests;

public class ObjectValidationTests
{
    [Test]
    public void CreateDeveloperWithoutEMail()
    {
        Assert.Throws<ArgumentNullException>(() => new Developer("J", "Doe", null));
    }
}