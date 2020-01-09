using AGames.Spillz.Scripts.Manager;
using DG.Tweening;
using ProjectCore.Input;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace AGames.Spillz.Scripts.Input.InputProcessors
{
    public class RayCastArgs : IInputArgs
    {
        public readonly Transform _PointRightToRay, _PointLeftToRey;
        public readonly Camera CameraMain;
        public readonly LayerMask LayerMask;
        public readonly float Duration;
        public readonly AnimationCurve CurveFrequency, CurveAmplitude;
//        public readonly HandHighlightController HandHighlight;

        public bool _highlightMode;
        private GameManager _gameManager;
        
        

        public RaycastHit _hit { get; private set; } 

        public RayCastArgs()
        {
            _gameManager = ServiceLocator.Resolve<GameManager>();
            _PointRightToRay = _gameManager.PointRightToRay;
            _PointLeftToRey = _gameManager.PointLeftToRay;
            CameraMain = _gameManager.Camera;
            LayerMask = _gameManager.LayerMaskForBlocks;
            Duration = _gameManager.CoolDown;
            CurveFrequency = _gameManager.AnimationCurveFrequency;
            CurveAmplitude = _gameManager.AnimationCurveAmplitude;
//            HandHighlight = _gameManager.HandHighlight;
        }

        public void SetHitInfo(RaycastHit hit, bool highlightMode)
        {
            _hit = hit;
            _highlightMode = highlightMode;
        }
    }
    
    public class InputProcessorRayCastEditor : InputBaseSpillzEditor<RayCastArgs>
    {
        private Camera _camera;
        private RaycastHit hit;
        private bool _highlightMode;
        
        protected override RayCastArgs Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.H)) _highlightMode = !_highlightMode;
            if (!UnityEngine.Input.GetMouseButtonDown(0)) return null;
            
            Debug.Log("MouseClick");
            var args = new RayCastArgs();
            _camera = args.CameraMain;
            
            Ray ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10f, args.LayerMask))
            {
                Debug.Log(hit.collider.name);
                args.SetHitInfo(hit, _highlightMode);
            }

            return args;
        }
    }
    
    public class InputProcessorRayCastOculus : InputBaseSpillzOculus<RayCastArgs>
    {
        private Transform _rayStartPoint;
        private RaycastHit hit;
        private bool CoolDown = true, _highlightMode;
        private float _frequency = 0.4f, _amplitude = 0.1f;
        
        protected override RayCastArgs Update()
        {
            var buttonHandRight = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);
            var buttonHandLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger);
            
            var buttonIndexRight = OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
            var buttonIndexLeft = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);

            var triggerRight = OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger);
            var triggerLeft = OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger);
            
            var leftRotate = buttonHandLeft && buttonIndexLeft;
            var rightRotate = buttonHandRight && buttonIndexRight;

            _highlightMode = OVRInput.GetDown(OVRInput.Button.Two) ? !_highlightMode : _highlightMode;
            
            if (leftRotate || rightRotate)
            {
                CoolDown = false;
                Timer.Register(0.3f, () => CoolDown = true);
            }
            
            if (buttonHandRight && CoolDown && !triggerRight && !buttonIndexRight)
            {
                var args = new RayCastArgs();
                _rayStartPoint = args._PointRightToRay;
                PushRayCats(OVRInput.Controller.RTouch, _rayStartPoint, ref args);
                return args;
            }
            
            if (buttonHandLeft && CoolDown && !triggerLeft && !buttonIndexLeft)
            {
                var args = new RayCastArgs();
                _rayStartPoint = args._PointLeftToRey;
                PushRayCats(OVRInput.Controller.LTouch, _rayStartPoint, ref args);
                return args;
            }
            
            return null;
        }
        
        private void PushRayCats(OVRInput.Controller controller, Transform point, ref RayCastArgs args)
        {
            var ray = new Ray(point.position, point.forward);

            if (Physics.Raycast(ray, out hit, 0.073f, args.LayerMask))
            {
                CoolDown = false; 
                Timer.Register(args.Duration, () => CoolDown = true,
                    null, false, false, null);

                DOTween.To(() => _amplitude, x => _amplitude = x, 0f, 0.15f)
                    .SetEase(args.CurveAmplitude);

                DOTween.To(() => _frequency, x => SetFrequency(x, controller), 0f, 0.16f)
                    .SetEase(args.CurveFrequency).OnComplete(() =>
                    {
                        OVRInput.SetControllerVibration(0f, 0f, controller);
                        _amplitude = 0.1f;
                        _frequency = 0.4f;
                    });
                
                args.SetHitInfo(hit, _highlightMode);
            }
            else args = null;
        }
        
        private void SetFrequency(float x, OVRInput.Controller controller)
        {
            OVRInput.SetControllerVibration(x, _amplitude, controller);
            _frequency = x;
        }
    }
}