using System;
using ProjectCore.Misc;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace AGames.Spillz.Scripts.Manager
{
    public class CameraManager : CachedBehaviour, IService
    {
        public Type ServiceType => typeof(CameraManager);

        [SerializeField] private Camera _editorCamera;
        [SerializeField] private Transform _ovrCameraTransform;
        [SerializeField] private Camera _centerEye;
    
       

        private Camera _mainCamera;

        private void Awake()
        {
#if UNITY_EDITOR
            _editorCamera.gameObject.SetActive(true);
            _ovrCameraTransform.gameObject.SetActive(false);
            _mainCamera = _editorCamera;
#elif UNITY_ANDROID
        _editorCamera.gameObject.SetActive(false);
        _ovrCameraTransform.gameObject.SetActive(true);
        _mainCamera = _centerEye;
#endif
            if (UnityObjectUtils.IsNull(_mainCamera)) 
                throw new NullReferenceException("[CameraManager] Сurrent platform is not supported. Main camera link == null");
        }

        public Camera ResolveCamera()
        {
            return _mainCamera;
        }

    }
}
