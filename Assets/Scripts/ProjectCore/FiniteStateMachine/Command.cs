using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ProjectCore.Misc;
using UnityEngine;

namespace ProjectCore.FiniteStateMachine
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public abstract class Command : CachedBehaviour
    {
        public StateMachine StateMachine;

        public bool IsRunning { get; private set; } = true;
        public bool IsStarted { get; private set; }
        public bool ResourcesReleased { get; private set; }

        public List<Command> ChildStates { get; } = new List<Command>();
        public Command DestroyHandler { get; private set; }

        public event Action<Command> OnResolve;
        public event Action<Command> OnReject;

        protected virtual void Start()
        {
            if (!IsRunning) return;

            IsStarted = true;
            OnLoad();
        }

        private void Update()
        {
            if (IsRunning) OnUpdate();
        }

        protected virtual void OnLoad()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnReleaseResources()
        {
        }

        public void Terminate()
        {
            FinishCommand(false);
        }

        public void FinishCommand(bool success)
        {
            if (!IsRunning) return;

            IsRunning = false;
            OnFinishCommand(success);
            Destroy(this);
        }

        private void OnCommandDestroy(Command command)
        {
            var commandId = command.GetInstanceID();
            var removeIndex = ChildStates.FindIndex(x => x.GetInstanceID().Equals(commandId));
            if (removeIndex != -1) ChildStates.RemoveAt(removeIndex);
        }

        private void OnFinishCommand(bool success)
        {
            foreach (var childCommand in ChildStates)
                childCommand.FinishCommand(success);

            if (success) OnResolve?.Invoke(this);
            else OnReject?.Invoke(this);
        }

        private void OnDestroy()
        {
            var stateName = GetType().Name;
            if (DestroyHandler.IsNotNull()) Debug.Log("Sub-State <color=green>" + stateName + "</color> destroyed.");
            else Debug.Log("State <color=green>" + stateName + "</color> destroyed.");

            if (!ResourcesReleased && IsStarted)
            {
                if (DestroyHandler.IsNotNull()) DestroyHandler.OnCommandDestroy(this);

                try
                {
                    OnReleaseResources();
                }
                catch (Exception e)
                {
                    Debug.LogError("Error during release command resources " + e);
                }

                ResourcesReleased = true;
            }
            else
            {
                Debug.LogError(
                    $"Resource release for State {stateName} skipped, started:{IsStarted}, released:{ResourcesReleased}");
            }
        }

        public static Command ExecuteOn<T>(GameObject target, Command command = null) where T : Command
        {
            return ExecuteOn(typeof(T), target, command);
        }

        public static Command ExecuteOn(Type type, GameObject target, Command command = null)
        {
            var newCommand = (Command) target.AddComponent(type);

            if (command.IsNull()) return newCommand;

            command.ChildStates.Add(newCommand);
            newCommand.DestroyHandler = command;
            newCommand.StateMachine = command.StateMachine;

            return command;
        }
    }
}