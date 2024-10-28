namespace System;

using System.Runtime.CompilerServices;

/// <summary>
/// Convenience APIs for "Design by Contract".
/// "Requires" APIs should be used to define pre-conditions.
/// "Invariant" APIs should be used to define invariants.
/// "Ensures" APIs should be used to define post-conditions.
/// </summary>
public static class Contract
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Requires(bool condition, [CallerArgumentExpression("condition")] string message = "")
    {
        if (!condition)
        {
            throw new ArgumentException(message);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RequiresNotNull<T>(T argument, [CallerArgumentExpression("argument")] string argumentName = "")
    {
        if (argument == null)
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RequiresNotNullNotEmpty(string argument, [CallerArgumentExpression("argument")] string argumentName = "")
    {
        if (string.IsNullOrEmpty(argument))
        {
            throw new ArgumentNullException($"String must not be null or empty: {argumentName}");
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RequiresNotNullNotEmpty<T>(IEnumerable<T> argument, [CallerArgumentExpression("argument")] string argumentName = "")
    {
        if (argument == null)
        {
            throw new ArgumentNullException(argumentName);
        }
        if (!argument.Any())
        {
            throw new ArgumentException($"Collection must not be empty: {argumentName}");
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Invariant(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Ensures<T>(T returnValue, Func<T, bool> validator, string message)
    {
        if (!validator(returnValue))
        {
            throw new ReturnValueException(message);
        }
        return returnValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsuresNotNull<T>(T returnValue) where T : class
    {
        if (returnValue == null)
        {
            throw new ReturnValueException("Return value must not be null");
        }
        return returnValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> EnsuresNotNullNotEmpty<T>(IEnumerable<T> returnValue)
    {
        if (returnValue == null)
        {
            throw new ReturnValueException("Return value must not be null");
        }
        if (!returnValue.Any())
        {
            throw new ReturnValueException("Return value must not be an empty collection");
        }
        return returnValue;
    }

    public class ReturnValueException : Exception
    {
        public ReturnValueException()
        {
        }

#pragma warning disable CC0001 // Constructor parameters must have guards
        public ReturnValueException(string message) : base(message)
        {
        }

        public ReturnValueException(string message, Exception innerException) : base(message, innerException)
        {
        }
#pragma warning restore CC0001 // Constructor parameters must have guards
    }
}
