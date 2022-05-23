using System.Collections.Generic;
using System.Linq;

namespace AboutCleanCode
{
    public class BusinessComponent
    {
        public double Compute(IEnumerable<KeyValuePair<string, string>> input)
        {
            Contract.RequiresNotNull(input,nameof(input));
            Contract.Requires(input.Count() > 0, "input requires to have at least one element");
            
            // TODO: why first? why not second or third?
            var item = input.FirstOrDefault().Key;

            var result = 0.0;

            // ... more business logic goes here ...

            return result;
        }
    }
}
