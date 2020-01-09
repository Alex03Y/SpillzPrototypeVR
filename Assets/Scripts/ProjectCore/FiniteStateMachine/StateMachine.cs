using System;
using UnityEngine;

namespace ProjectCore.FiniteStateMachine
{
    public class StateMachine
    {
        private GameObject _container;

        /// <summary>
        /// Called after applying the new State.
        /// </summary>
        public event Action<Type> OnStateChanged;

        public StateMachine(GameObject container)
        {
            _container = container;
        }

        public Command ActiveCommand { get; private set; }

        public void ApplyState<T>() where T : Command
        {
            ChangeState(typeof(T));
            OnStateChanged?.Invoke(typeof(T));
        }

        public void ApplyState(Type type)
        {
            ChangeState(type);
            OnStateChanged?.Invoke(type);
        }

        public Command Execute(Type type)
        {
            return Command.ExecuteOn(type, _container, ActiveCommand);
        }

        public Command Execute<T>() where T : Command
        {
            return Command.ExecuteOn(typeof(T), _container, ActiveCommand);
        }

        private void ChangeState(Type type)
        {
            var prevState = ActiveCommand;
            ActiveCommand = null;
            if (prevState != null) prevState.FinishCommand(true);
            ActiveCommand = CreateState(type);
        }

        private Command CreateState(Type type)
        {
            var state = (Command) _container.AddComponent(type);
            state.StateMachine = this;

            return state;
        }
    }
}