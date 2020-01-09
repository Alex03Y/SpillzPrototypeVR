using ProjectCore.Input;
using UnityEngine;

namespace AGames.Spillz.Scripts.Input.InputProcessors
{
    public class MouseScrollArgs : IInputArgs
    {
        public readonly float ScrollValue;
    
        public MouseScrollArgs(float scrollValue)
        {
            ScrollValue = scrollValue;
        }
    }

    public class InputProcessorMouseScroll : InputBaseSpillzEditor<MouseScrollArgs>
    {
        protected override MouseScrollArgs Update()
        {
            var input = UnityEngine.Input.GetAxis("Mouse ScrollWheel"); 
            if (Mathf.Abs(input)>= 0.01f)
                return new MouseScrollArgs(input);
            return null;
        }
    }
}