using NUnit.Framework;

namespace AboutCleanCode
{
    [TestFixture]
    public class RepositoryTests
    {
        [Test]
        public void LinQ_WillWork()
        {
            var repository = new Repository();

            // just a few more calls to have higher "transaction count"
            var isNotEmpty = System.Linq.Enumerable.Any(repository.FindAll());
            isNotEmpty = System.Linq.Enumerable.Any(repository.FindAll());
            isNotEmpty = System.Linq.Enumerable.Any(repository.FindAll());

            Assert.That(isNotEmpty, Is.True);
            Assert.That(repository.TransactionsCount, Is.EqualTo(0));
        }

        [Test]
        public void BrokenCustomLinQ_WillFail()
        {
            var repository = new Repository();

            // just a few more calls to have higher "transaction count"
            var isNotEmpty = AboutCleanCode.EnumerableExtensions.Any_Broken(repository.FindAll());
            isNotEmpty = AboutCleanCode.EnumerableExtensions.Any_Broken(repository.FindAll());
            isNotEmpty = AboutCleanCode.EnumerableExtensions.Any_Broken(repository.FindAll());

            Assert.That(isNotEmpty, Is.True);
            Assert.That(repository.TransactionsCount, Is.EqualTo(0));
        }

        [Test]
        public void FixedCustomLinQ_WillWork()
        {
            var repository = new Repository();

            // just a few more calls to have higher "transaction count"
            var isNotEmpty = AboutCleanCode.EnumerableExtensions.Any(repository.FindAll());
            isNotEmpty = AboutCleanCode.EnumerableExtensions.Any(repository.FindAll());
            isNotEmpty = AboutCleanCode.EnumerableExtensions.Any(repository.FindAll());

            Assert.That(isNotEmpty, Is.True);
            Assert.That(repository.TransactionsCount, Is.EqualTo(0));
        }
    }
}
