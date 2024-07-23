namespace WarningRadar.Tests;

[TestFixture]
public class ServiceLocatorTests
{
    [Test]
    public void Resolve_WithRegisteredService_ShouldReturnService()
    {
        var serviceLocator = new ServiceLocator();
        var service = new DummyService();
        serviceLocator.Register<IDummyService>(service);

        var resolvedService = serviceLocator.Resolve<IDummyService>();

        Assert.That(resolvedService, Is.EqualTo(service));
    }

    [Test]
    public void Resolve_WithUnregisteredService_ShouldThrowException()
    {
        var serviceLocator = new ServiceLocator();

        Assert.Throws<InvalidOperationException>(() => serviceLocator.Resolve<IDummyService>());
    }

    [Test]
    public void TryResolve_WithRegisteredService_ShouldReturnService()
    {
        var serviceLocator = new ServiceLocator();
        var service = new DummyService();
        serviceLocator.Register<IDummyService>(service);

        var resolvedService = serviceLocator.TryResolve<IDummyService>();

        Assert.That(resolvedService, Is.EqualTo(service));
    }

    [Test]
    public void TryResolve_WithUnregisteredService_ShouldReturnDefault()
    {
        var serviceLocator = new ServiceLocator();

        var resolvedService = serviceLocator.TryResolve<IDummyService>();

        Assert.That(resolvedService, Is.EqualTo(default(IDummyService)));
    }
}

public interface IDummyService
{
    void DoSomething();
}

public class DummyService : IDummyService
{
    public void DoSomething()
    {

    }
}
