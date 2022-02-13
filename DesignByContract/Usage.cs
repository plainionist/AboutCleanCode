using System.Collections.Generic;

namespace AboutCleanCode
{
    public class MyComponent
    {
        public void Execute(string path)
        {
            Contract.RequiresNotNull(path, nameof(path));

            // business logic goes here ...
        }

        public IEnumerable<string> GetItems()
        {
            var items = new List<string>();

            // business logic goes here ...

            return Contract.EnsuresNotNull(items);
        }

        public IEnumerable<string> GetMinimumItems()
        {
            var items = new List<string>();

            // business logic goes here ...

            return Contract.Ensures(items, x => x.Count > 0, "Return value must contain at least one element");
        }
    }
}