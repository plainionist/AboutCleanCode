using System;
using System.Collections;

namespace AboutCleanCode
{
    public static class EnumerableExtensions
    {
        public static bool Any_Broken(this IEnumerable self)
        {
            return  self.GetEnumerator().MoveNext();
        }

        public static bool Any(this IEnumerable self)
        {
            var enumerator = self.GetEnumerator();
            try
            {
                return enumerator.MoveNext();
            }
            finally  
            {
                // important to call dispose if implemented by enumerator to support "yield return" in "try-finally"
                (enumerator as IDisposable)?.Dispose();
            } 
        }
    }
}
