using System.Collections.Generic;
using ImpactAnalyzer.Entities;

namespace ImpactAnalyzer
{
    class Analyzer
    {
        public void Analyze(IReadOnlyCollection<CodeType> types)
        {
            foreach (var type in types)
            {
                // ...

                if (IsThirdParty(type.Assembly))
                {
                    // ...
                }

                // ...
            }
        }

        private bool IsThirdParty(Assembly assembly)
        {
            // ...

            return true;
        }
    }
}
