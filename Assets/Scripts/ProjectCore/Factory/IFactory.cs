using System;
using ProjectCore.FiniteStateMachine;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace ProjectCore.Factory
{
    public interface IFactory : IService
    {
        
    }

    public interface IFactory<out TReturn, in TArgs> : IFactory where TArgs : IFactoryArgs
    {
        TReturn Create(TArgs args);
    }

    public interface IFactoryArgs
    {
    }

    // sample usage
    public class StateMachineFactory : IFactory<StateMachine, StateMachineFactory.StateMachineArgs>
    {
        public Type ServiceType => typeof(IFactory<StateMachine, StateMachineArgs>);

        public class StateMachineArgs : IFactoryArgs
        {
            public readonly GameObject Container;

            public StateMachineArgs(GameObject container)
            {
                Container = container;
            }
        }

        public StateMachine Create(StateMachineArgs args)
        {
            return new StateMachine(args.Container);
        }
    }
}