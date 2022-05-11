using System;
using System.Collections.Generic;
using System.Linq;

namespace TestFailureAnalyzer.Core
{
    /// <summary>
    /// Convenience APIs for "Design by Contract"
    /// </summary>
    public static class Contract
    {
        public static void RequiresNotNull<T>(T argument, string argumentName) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void RequiresNotNullNotEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullException($"String must not null or empty: {argumentName}");
            }
        }

        public static void RequiresNotNullNotEmpty<T>(IEnumerable<T> collection, string argumentName)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(argumentName);
            }
            
            if (!collection.Any())
            {
                throw new ArgumentException($"Collection must not be empty: {argumentName}");
            }
        }
    }
}
