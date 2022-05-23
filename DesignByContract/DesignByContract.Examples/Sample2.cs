using System.Collections.Generic;
using System.Linq;

namespace AboutCleanCode
{
    public class UseCase
    {
        public IRepository myRepository;

        public UseCase(IRepository repository)
        {
            Contract.RequiresNotNull(repository, nameof(repository));

            myRepository = repository;
        }

        public int Process(IConfiguration configuration)
        {
            Contract.RequiresNotNull(configuration, nameof(configuration));

            var pattern = configuration.GetPattern();

            Contract.Requires(pattern != null, "Pattern is expected to be not null");

            var relevantItems = myRepository.GetAllItems()
                .Where(x => x.StartsWith(pattern))
                .ToList();

            return Contract.Ensures(relevantItems.Max(x => x.Length),
                r => r > -1, "Return value should have a positive value");
        }
    }











    public interface IRepository
    {
        IReadOnlyCollection<string> GetAllItems();
    }

    public interface IConfiguration
    {
        string GetPattern();
    }
}
