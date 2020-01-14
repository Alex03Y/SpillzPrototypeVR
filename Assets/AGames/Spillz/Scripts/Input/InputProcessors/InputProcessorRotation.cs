using AGames.Spillz.Scripts.Controllers;
using AGames.Spillz.Scripts.Manager;
using ProjectCore.Input;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace AGames.Spillz.Scripts.Input.InputProcessors
{
    public class RotationArgs : IInputArgs
    {
        public float Rotation { get; private set; }
        public readonly TowerControllerForRotation Target;
        private GameManager _gameManager;

        public RotationArgs()
        {
            _gameManager = ServiceLocator.Resolve<GameManager>();
            Target = _gameManager.RotationControllerForRotation;
        }

        public void SetRotate(float rotation)
        {
            Rotation = rotation;
        }
    }

    public class InputProcessorRotationEditor : InputBaseSpillzEditor<RotationArgs>
    {
        private Vector3 _rotation;
        protected override RotationArgs Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Debug.Log("Cursor Locked");
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("Cursor Unlocked");
            }

            if (Cursor.lockState == CursorLockMode.Locked)
            {
                _rotation.y = UnityEngine.Input.GetAxis("Mouse X");
                _rotation.x = UnityEngine.Input.GetAxis("Mouse Y");

//                if (Mathf.Abs(_rotation.x) > 0.01f || Mathf.Abs(_rotation.y) > 0.01f)
//                {
//                    return new RotationArgs(Quaternion.Euler(_rotation));
//                }
            }

            return null;
        }
    }

    public class InputProcessorRotationVelocity : InputBaseSpillzOculus<RotationArgs>
    {
        private bool _grabing;
        private RotationArgs _args;
        private float _valueForVibration = 0.5f;
        private bool _vibrationOn = true;
        
        protected override RotationArgs Update()
        {
            var grabHandLeftDown = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) > 0.1f;
            var grabIndexLeftDown = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0.1f;

//            var grabHandLeftUp = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch) < 0.1f;
//            var grabIndexLeftUp = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) < 0.1f;

            var grabbingNow = grabHandLeftDown && grabIndexLeftDown;
            
            if (grabbingNow && !_vibrationOn)
            {
                _grabing = true;
                Grabering();
                _vibrationOn = true;
            }
            else if (!grabbingNow && _vibrationOn)
            {
                _grabing = false;
                _vibrationOn = false;
                OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
                _args.Target.EndRotation();
            }

            if (_grabing)
            {
                var velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
                _args.SetRotate(-velocity.x);
                var velocityToVibration = Mathf.Clamp(Mathf.Abs(velocity.x) / 3f, 0f, 0.1f);
                 OVRInput.SetControllerVibration(velocityToVibration * 1.2f, velocityToVibration * 0.8f, OVRInput.Controller.LTouch);
                return _args;
            }
            
            void Grabering()
            {
                _args = new RotationArgs();
                _args.Target.StartRotate();
                _grabing = true;
            }

            return null;
        }
    }
    
    public class InputProcessorRotationOculus : InputBaseSpillzOculus<RotationArgs>
    {
        private Vector3 _rotation, prevPosition, _rotationVelocity;
        private bool IsGrabb = false;
        private Transform _leftAnchore;
        private RotationArgs _args;
        private float currentDirection;
        
        protected override RotationArgs Update()
        {
            var grabHandLeft = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) > 0.1f ;
            var grabIndexLeft = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0.1f;
            
            var grabHandRight = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);
            var grabIndexRight = OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);

            if (grabHandLeft && grabIndexLeft && !IsGrabb)
            {
                currentDirection = 1f;
                Grabering();
            }

            if (grabHandRight && grabIndexRight && !IsGrabb)
            {
                currentDirection = -1f;
                Grabering();
            }

            var leftBreakeGrabb = IsGrabb && !grabHandRight && !grabHandLeft || !grabIndexLeft;
            var rightBreakeGrabb = IsGrabb && !grabHandLeft && !grabHandRight || !grabIndexRight;
            

            if ( leftBreakeGrabb || rightBreakeGrabb )
            {
                IsGrabb = false;
                _args.Target.EndRotation();
            }

            if (IsGrabb)
            {
//                var v = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
                _args.SetRotate(currentDirection);
                return _args;
            }

            void Grabering()
            {
                _args = new RotationArgs();
                _args.Target.StartRotate();
                IsGrabb = true;
            }
            
            return null;
        }
    }
}