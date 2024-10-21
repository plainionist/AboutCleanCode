using System;
using System.Collections.Generic;
using System.Linq;

namespace System;

public record Option<T> : Option
{
    private readonly T myValue;

    public Option(T value)
    {
        myValue = value;
    }

    public Option(string failure)
    {
        if (string.IsNullOrEmpty(failure)) throw new ArgumentNullException(nameof(failure));

        Failure = failure;
    }

    public T Value => IsSome ? myValue : throw new InvalidOperationException(Failure);

    public override string ToString() => IsSome ? $"Some({myValue})" : $"None({Failure})";

    public string Failure { get; }

    public Option<T> Select(Func<T, T> f) => IsSome ? Some(f(myValue)) : None<T>(Failure);
    public Option<U> Select<U>(Func<T, U> f) => IsSome ? Some(f(myValue)) : None<U>(Failure);
    public Option<U> Select<U>(Func<T, Option<U>> f) => IsSome ? f(myValue) : None<U>(Failure);

    public Option<bool> Where<U>(Func<T, bool> f) => IsSome ? Some(f(myValue)) : None<bool>(Failure);

    public T Default(T value) => IsSome ? myValue : value;
    public T Default(Func<T> f) => IsSome ? myValue : f();
    public T Default(Func<string, T> f) => IsSome ? myValue : f(Failure);

    public void Match(Action<T> some) => Match(some, () => { });
    public void Match(Action<T> some, Action none) => Match(some, f => none());
    public void Match(Action<T> some, Action<string> none)
    {
        if (IsSome)
        {
            some(myValue);
        }
        else
        {
            none(Failure);
        }
    }
}

public record Option
{
    public bool IsSome { get; private set; }
    public bool IsNone => !IsSome;

    public static Option<T> Some<T>(T value) =>
        new Option<T>(value)
        {
            IsSome = true
        };
    public static Option<T> None<T>(string message) =>
        new Option<T>(message)
        {
            IsSome = false
        };
    public static Option<T> OfObject<T>(T value, string message) =>
        value != null ? Some(value) : None<T>(message);
    public static Option<T> OfNullable<T>(T? value, string message) where T : struct =>
        value.HasValue ? Some(value.Value) : None<T>(message);
}

public static class OptionExtensions
{
    public static IEnumerable<U> Choose<T, U>(this IEnumerable<T> self, Func<T, Option<U>> f) =>
        self
            .Select(f)
            .Where(x => x.IsSome)
            .Select(x => x.Value);
}
