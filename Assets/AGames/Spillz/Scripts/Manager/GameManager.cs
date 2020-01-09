using System;
using AGames.Spillz.Scripts.Controllers;
using ProjectCore.Misc;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace AGames.Spillz.Scripts.Manager
{
    public class GameManager : CachedBehaviour, IService
    {
        public Type ServiceType => typeof(GameManager);

        [SerializeField] private Transform _rightHand;
        public Transform RightHand => _rightHand;
        
        [SerializeField] private Camera _camera;
        public Camera Camera => _camera;
        
        [SerializeField] private Transform _target;
        public Transform Target => _target;
        
        [SerializeField] private string _nameLayer;
        public LayerMask LayerMaskForBlocks => LayerMask.GetMask(_nameLayer);

        [SerializeField] private Transform _leftHand;
        public Transform LeftHand => _leftHand;

        [SerializeField] private Transform _leftAnchore;
        public Transform LeftAnchore => _leftAnchore;

        [SerializeField] private Transform _rightAnchore;
        public Transform RightAnchore => _rightAnchore;

        [SerializeField] private Transform _pointRightToRay;
        public Transform PointRightToRay => _pointRightToRay;
        
        [SerializeField] private Transform _pointLeftToRay;
        public Transform PointLeftToRay => _pointLeftToRay;

        [SerializeField] private float _coolDown;
        public float CoolDown => _coolDown;

//        [SerializeField] private ManagerLevel _levelManager;
//        public ManagerLevel LevelManager => _levelManager;

        [SerializeField] private TowerRotateController _rotationController;
        public TowerRotateController RotationController => _rotationController;

        [SerializeField] private AnimationCurve _animationCurveFrequency;
        public AnimationCurve AnimationCurveFrequency => _animationCurveFrequency;
        
        [SerializeField] private AnimationCurve _animationCurveAmplitude;
        public AnimationCurve AnimationCurveAmplitude => _animationCurveAmplitude;

        [SerializeField] private string _tagName;
        public string TagName => _tagName;

        [SerializeField] private HandHighlightController _hands;
        public HandHighlightController HandHighlight => _hands;


        private void Awake()
        {
            Application.targetFrameRate = 120;
        }
    }
}