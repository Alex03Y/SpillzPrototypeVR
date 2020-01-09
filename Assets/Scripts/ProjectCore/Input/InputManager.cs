using System;
using System.Collections.Generic;
using System.Linq;
using ProjectCore.Misc;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace ProjectCore.Input
{
    public class InputManager : CachedBehaviour, IService
    {
        public Type ServiceType => typeof(InputManager);

        private readonly Dictionary<Type, List<InputReceiver>> _listeners =
            new Dictionary<Type, List<InputReceiver>>();

        private readonly List<InputProcessorBase> _inputProcessors = new List<InputProcessorBase>();

        private void Start()
        {
            var currentPlatform = Application.platform;
            var customInputs = GetComponent<BaseInputRegisterer>();

            if (customInputs != null)
            {
                customInputs.GetInputs(out var inputs);
                inputs.ForEach(x =>
                {
                    foreach (var inputProcessor in x.Inputs)
                    {
                        if(inputProcessor.RuntimePlatforms.Contains(currentPlatform))
                            _inputProcessors.Add(inputProcessor);
                    }
                });
            }
            
            Debug.Log($"[<color=green>InputManager</color>] Input processors find: {_inputProcessors.Count}");
        }

        public InputReceiver<TArgs> Subscribe<TArgs>(InputReceiver<TArgs> listener) where TArgs : IInputArgs
        {
            var key = typeof(TArgs);

            if (_listeners.TryGetValue(key, out var listeners)) listeners.Add(listener);
            else _listeners.Add(key, new List<InputReceiver> {listener});

            return listener;
        }

        public void Unsubscribe<TArgs>(InputReceiver<TArgs> listener) where TArgs : IInputArgs
        {
            var key = typeof(TArgs);

            if (_listeners.TryGetValue(key, out var listeners))
                listeners.Remove(listener);
        }

        public void Unsubscribe<TArgs>(IInputReceiver<TArgs> listener) where TArgs : IInputArgs
        {
            var key = typeof(TArgs);

            if (_listeners.TryGetValue(key, out var listeners))
            {
                if (listener is CachedBehaviour receiver)
                {
                    var id = receiver.InstanceId;
                    while (listeners.FindIndex(x => x.InstanceId == id) is var index && index != -1)
                        listeners.RemoveAt(index);
                }
                else Debug.LogError($"Target should be inherited from {nameof(CachedBehaviour)}.");
            }
        }

        private void Update()
        {
            foreach (var inputProcessor in _inputProcessors)
            {
                if (!inputProcessor.UpdateInternal(out var args)) continue;

                var key = inputProcessor.ArgsType;
                if (!_listeners.TryGetValue(key, out var listeners)) continue;

                foreach (var listener in listeners)
                    listener.Execute(args);
            }
        }
    }
}