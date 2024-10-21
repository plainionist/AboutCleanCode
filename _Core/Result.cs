using System.Collections.Generic;

namespace System;

public readonly struct Result<TValue, TError>
{
    private readonly TValue myValue;
    private readonly TError myError;

    public Result(TValue value)
    {
        Contract.RequiresNotNull(value);

        myValue = value;
        myError = default;
    }

    public Result(TError error)
    {
        Contract.RequiresNotNull(error);

        myError = error;
        myValue = default;
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);
    public static implicit operator Result<TValue, TError>(TError error) => new(error);

    public TResult Select<TResult>(Func<TValue, TResult> onSuccess, Func<TError, TResult> onError) =>
        !EqualityComparer<TError>.Default.Equals(myError, default) ? onError(myError) : onSuccess(myValue);

    public void Match(Action<TValue> onSuccess, Action<TError> onError)
    {
        if (!EqualityComparer<TError>.Default.Equals(myError, default))
        {
            onError(myError);
        }
        else
        {
            onSuccess(myValue);
        }
    }
}
