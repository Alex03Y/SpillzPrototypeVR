using System;
using AGames.Spillz.Scripts.Input.InputProcessors;
using AGames.Spillz.Scripts.Manager;
using ProjectCore.Input;
using ProjectCore.Misc;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace AGames.Spillz.Scripts.Controllers
{
    public class CameraEditorController : CachedBehaviour, IInputReceiver<MovementArg>, IInputReceiver<MouseScrollArgs>
    {
    
        [SerializeField] private float distance = 5.0f;
        [SerializeField] private float scrollSpeed = 5f;
        
        [SerializeField] private float xSpeed = 120.0f;
        [SerializeField] private float ySpeed = 120.0f;
  
        [SerializeField] private float yMinLimit = 0f;
        [SerializeField] private float yMaxLimit = 80f;
  
        [SerializeField] private float distanceMin = 0.5f;
        [SerializeField] private float distanceMax = 15f;
  
        
        //private Rigidbody rigidbody;

        private Transform _target;
        private GameManager _gameManager;
        private InputManager _inputManager;
        private float _x;
        private float _y;
        private Quaternion _rotation;
  
        // Use this for initialization
        void Start ()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            _gameManager = ServiceLocator.Resolve<GameManager>();
            _inputManager = ServiceLocator.Resolve<InputManager>();
            
            _inputManager.Subscribe(new InputReceiver<MovementArg>(this));
            _inputManager.Subscribe(new InputReceiver<MouseScrollArgs>(this));
            
            _target = _gameManager.Target;
            
            if (_target == null) throw new Exception("[CameraController] You must set target object");
           
            Vector3 angles = Transform.Value.eulerAngles;
            _x = angles.y;
            _y = angles.x;
        }
  
        private void SetChangeMovement() 
        {
//                RaycastHit hit;
//                if (Physics.Linecast (_target.position, transform.position, out hit)) 
//                {
//                    distance -=  hit.distance;
//                }

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position =  _rotation * negDistance + _target.position; 
  
            Transform.Value.rotation = _rotation;
            Transform.Value.position = position;
            
        }
  
        public void Execute(MovementArg args)
        {
            _x -= args._directMove.x * xSpeed * distance * Time.deltaTime;
            _y += args._directMove.y * ySpeed * Time.deltaTime;
  
            _y = ClampAngle(_y, yMinLimit, yMaxLimit);
            
            _rotation = Quaternion.Euler(_y, _x, 0);
            SetChangeMovement();
        }

        public void Execute(MouseScrollArgs args)
        {
            distance = Mathf.Clamp(distance - args.ScrollValue * scrollSpeed * Time.deltaTime, distanceMin, distanceMax);
            SetChangeMovement();
        }

        private void OnDestroy()
        {
            _inputManager.Unsubscribe<MovementArg>(this);
            _inputManager.Unsubscribe<MouseScrollArgs>(this);
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360f)
                angle += 360f;
            if (angle > 360f)
                angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }
    }
}
