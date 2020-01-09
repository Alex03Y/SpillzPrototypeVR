using ProjectCore.Misc;
using UnityEngine;

namespace ProjectCore.Input
{
    public interface IInputReceiver<in T> where T : IInputArgs
    {
        void Execute(T args);
    }

    public abstract class InputReceiver
    {
        public abstract int InstanceId { get; }
        public abstract void Execute(IInputArgs args);
    }

    public class InputReceiver<T> : InputReceiver where T : IInputArgs
    {
        public override int InstanceId => _instanceId;
        private readonly int _instanceId;
        private readonly IInputReceiver<T> _target;
        
        public InputReceiver(IInputReceiver<T> target)
        {
            _target = target;
            if (target is CachedBehaviour receiver) _instanceId = receiver.InstanceId;
            else Debug.LogError($"Target should be inherited from {nameof(CachedBehaviour)}.");
        }

        public override void Execute(IInputArgs inputArgs) => Execute((T) inputArgs);
        public void Execute(T args) => _target.Execute(args);
    }
}