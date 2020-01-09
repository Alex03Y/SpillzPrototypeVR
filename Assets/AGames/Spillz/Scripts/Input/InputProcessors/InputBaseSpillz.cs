using ProjectCore.Input;
using UnityEngine;

namespace AGames.Spillz.Scripts.Input.InputProcessors
{
    public abstract class InputBaseSpillzEditor<T> : InputProcessorBase<T> where T: IInputArgs
    {
        public override RuntimePlatform[] RuntimePlatforms => EditorPlatform;
    }

    public abstract class InputBaseSpillzOculus<T> : InputProcessorBase<T> where T : IInputArgs
    {
        public override RuntimePlatform[] RuntimePlatforms => AndroidPlatform;
    }
}