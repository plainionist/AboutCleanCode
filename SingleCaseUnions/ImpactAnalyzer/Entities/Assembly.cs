
using System;

namespace ImpactAnalyzer.Entities
{
    public record Assembly
    {
        private readonly string myValue;

        public Assembly(string value)
        {
            Contract.RequiresNotNullNotEmpty(value, nameof(value));

            myValue = value;
        }

        public bool Filter(Func<string, bool> f) => f(myValue);

        public Assembly Map(Func<string, string> f) => new Assembly(f(myValue));
    }
}
