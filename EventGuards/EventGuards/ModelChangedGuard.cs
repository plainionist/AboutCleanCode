using System;

namespace EventGuards
{
    internal class ModelChangedGuard : IDisposable
    {
        private Action<ModelChangedGuard> myRemoveAction;

        public ModelChangedGuard(Action<ModelChangedGuard> removeAction)
        {
            myRemoveAction = removeAction;
        }

        public void Dispose()
        {
            myRemoveAction?.Invoke(this);
            // ensure that multiple Dispose() calls do not do any harm
            myRemoveAction = null;
        }
    }
}