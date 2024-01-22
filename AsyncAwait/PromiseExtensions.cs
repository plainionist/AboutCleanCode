using System;
using System.Runtime.CompilerServices;

namespace AsyncAwait;

public static class PromiseExtensions
{
    public static IAwaiter GetAwaiter(this Promise self) =>
        new Awaiter(self);

    public interface IAwaiter : INotifyCompletion
    {
        bool IsCompleted { get; }
        Response GetResult();
    }

    private class Awaiter : IAwaiter
    {
        private readonly Promise myPromise;
        public Awaiter(Promise self) =>
            myPromise = self;

        public bool IsCompleted => myPromise.HasResult || myPromise.IsFaulted;
        public Response GetResult() => myPromise.Result;

        public void OnCompleted(Action continuation)
        {
            if (myPromise.IsFaulted) throw new Exception("Computation failed", myPromise.Exception);
            myPromise.ContinueWith(_ => continuation());
        }
    }
}