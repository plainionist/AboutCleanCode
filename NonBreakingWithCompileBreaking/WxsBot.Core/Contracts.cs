using System;
using System.Collections.Generic;
using System.Linq;

namespace WxsBot
{
    /// <summary>
    /// Convenience APIs for "Design by Contract"
    /// </summary>
    public static class Contract
    {
        public static T RequiresNotNull<T>(T argument, string argumentName) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
            return argument;
        }

        public static void Requires(bool condition, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message);
            }
        }

        public static void RequiresNotNullNotEmpty(string str, string argumentName)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException($"string must not null or empty: {argumentName}");
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

        public static void Invariant(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}
