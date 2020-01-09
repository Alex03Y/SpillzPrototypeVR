using System;
using UnityEngine;

namespace ProjectCore.Input
{
    public abstract class InputProcessorBase
    {
        public abstract Type ArgsType { get; }
        public abstract RuntimePlatform[] RuntimePlatforms { get; }
        public abstract bool UpdateInternal(out IInputArgs args);
        
        public static RuntimePlatform[] EditorPlatform =
        {
            RuntimePlatform.LinuxEditor,
            RuntimePlatform.OSXEditor,
            RuntimePlatform.WindowsEditor,
        };
        
        public static RuntimePlatform[] AndroidPlatform =
        {
            RuntimePlatform.Android,
        };
    }

    public abstract class InputProcessorBase<T> : InputProcessorBase where T : IInputArgs
    {
        public override Type ArgsType => typeof(T);

        public override bool UpdateInternal(out IInputArgs args)
        {
            args = Update();
            return args != null;
        }

        protected abstract T Update();
    }
}