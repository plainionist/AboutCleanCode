using NUnit.Framework;

namespace DocBot.Bot.Utsp.Specs.TDK
{
    [TestFixture]
    public abstract class TestBase<T> where T : Builder, new()
    {
        [SetUp]
        public void SetUp()
        {
            Builder = new T();
        }

        protected T Builder { get; private set; }

        protected IGiven Given => this.Builder;
        protected IWhen When => this.Builder;
        protected IThen Then => this.Builder;
    }
}
