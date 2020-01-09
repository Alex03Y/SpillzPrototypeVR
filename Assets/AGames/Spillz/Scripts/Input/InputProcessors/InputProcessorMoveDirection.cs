using ProjectCore.Input;
using UnityEngine;

namespace AGames.Spillz.Scripts.Input.InputProcessors
{
    public class MovementArg : IInputArgs
    {
        public readonly Vector2 _directMove;

        public MovementArg(Vector2 directionMove)
        {
            _directMove = directionMove;
        }
    }

    public class InputProcessorMoveDirectionEditor : InputBaseSpillzEditor<MovementArg>
    {
        private Vector2 _currentInput;
    
        protected override MovementArg Update()
        {
            _currentInput.x = UnityEngine.Input.GetAxis("Horizontal");
            _currentInput.y = UnityEngine.Input.GetAxis("Vertical");
            if ( Mathf.Abs(_currentInput.x) >= 0.01f || Mathf.Abs(_currentInput.y) >= 0.01f )
                return new MovementArg(_currentInput);
            return null;
        }
    }

    public class InputProcessorMoveDirectionOculus : InputBaseSpillzOculus<MovementArg>
    {
        private Vector2 _currentInput;
    
        protected override MovementArg Update()
        {
            _currentInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            if (Mathf.Abs(_currentInput.x) > 0.01f || Mathf.Abs(_currentInput.y) > 0.01f)
                return new MovementArg(Vector3.ClampMagnitude(_currentInput, 1f));
            return null;
        }
    }
}