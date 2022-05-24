using System;

namespace AboutCleanCode.CommonLibrary
{
    public interface IApplicationContext
    {
        event EventHandler<EventArgs> StartupCompleted;
    }
}