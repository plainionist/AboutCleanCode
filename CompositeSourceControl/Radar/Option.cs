using System;

namespace Radar;

public record Option<T> : Option
{
    private readonly T myValue;

    public Option(T value)
    {
        // TODO: any idea how to avoid "null"?
        //Contract.RequiresNotNull(value, nameof(value));

        myValue = value;
    }

    public Option(string failure)
    {
        Contract.RequiresNotNull(failure, nameof(failure));

        Failure = failure;
    }

    public T Value => IsSome ? myValue : throw new InvalidOperationException(Failure);

    public override string ToString() => IsSome ? $"Some({myValue})" : $"None({Failure})";

    public string Failure { get; }

    public Option<T> Select(Func<T, T> f) => IsSome ? Some(f(myValue)) : None<T>(Failure);
    public Option<U> Select<U>(Func<T, U> f) => IsSome ? Some(f(myValue)) : None<U>(Failure);
    public Option<U> Select<U>(Func<T, Option<U>> f) => IsSome ? f(myValue) : None<U>(Failure);
    /// <summary>
    /// Combines regular "Select" with "Default" to avoid unessary creation of an instance of the Option type.
    /// </summary>
    public U SelectOrDefault<U>(Func<T, U> f, U defaultValue) => IsSome ? f(myValue) : defaultValue;

    public Option<bool> Where<U>(Func<T, bool> f) => IsSome ? Some(f(myValue)) : None<bool>(Failure);

    public T Default(T value) => IsSome ? myValue : value;
    public T Default(Func<T> f) => IsSome ? myValue : f();
    public T Default(Func<string, T> f) => IsSome ? myValue : f(Failure);

    public Option<T> OrElse(Func<Option<T>> f) => IsSome ? this : f();

    public void ForEach(Action<T> f)
    {
        if (IsSome)
        {
            f(myValue);
        }
    }

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
}
