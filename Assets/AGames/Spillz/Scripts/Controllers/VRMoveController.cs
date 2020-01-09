using AGames.Spillz.Scripts.Input.InputProcessors;
using AGames.Spillz.Scripts.Manager;
using ProjectCore.Input;
using ProjectCore.Misc;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace AGames.Spillz.Scripts.Controllers
{
    public class VRMoveController : CachedBehaviour, IInputReceiver<RotationArgs> //, IInputReceiver<MovementArg>
    {
        private GameManager _gameManager;
        private InputManager _inputManager;
        private float _speedMove = 1.5f;
        private float _speedRotete = 4f;
        private float _x, _y, _moveX, _moveZ;
        private Vector3 _forvardMove, _rightMove;
        [SerializeField] private Transform _SpaceTracking;
        [SerializeField] private Transform _RotateTarget;
        

        private void Start()
        {
            _gameManager = ServiceLocator.Resolve<GameManager>();
            _inputManager = ServiceLocator.Resolve<InputManager>();
            _inputManager.Subscribe(new InputReceiver<RotationArgs>(this));
            
//            _forvardMove = _SpaceTracking.forward;
//            _rightMove = _SpaceTracking.right;
        }

//        public void Execute(MovementArg args)
//        {
//            var a = args._directMove;
//            _moveZ += args._directMove.y * Time.deltaTime * _speedMove;
//            _moveX += args._directMove.x * Time.deltaTime * _speedMove;
//            var prevPosition = _SpaceTracking.position;
//             prevPosition = new Vector3( _moveX, prevPosition.y, _moveZ);
//            _SpaceTracking.position = prevPosition;
//        }

        private Quaternion _rotation;

        public void Execute(RotationArgs args)
        {
            var rotate = args.Rotation * _speedRotete;
            _RotateTarget.rotation *= Quaternion.Euler(new Vector3(0f, rotate, 0f));
        }
    }
}
