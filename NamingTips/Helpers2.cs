using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Naming
{
    public class StringComparer : IEqualityComparer<string>
    {
        /// <summary>
        /// Evaluates whether the two strings are equal or not
        /// </summary>
        public bool Equals(string firstString, string secondString)
        {
            if (string.IsNullOrEmpty(firstString) || string.IsNullOrEmpty(secondString))
            {
                return false;
            }

            firstString = firstString.Trim().ToUpperInvariant();
            secondString = secondString.Trim().ToUpperInvariant();

            return firstString == secondString;
        }

        public int GetHashCode([DisallowNull] string obj) =>
            obj.Trim().ToUpperInvariant().GetHashCode();
    }
}
