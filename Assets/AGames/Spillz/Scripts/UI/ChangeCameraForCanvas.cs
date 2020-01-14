using AGames.Spillz.Scripts.Manager;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace AGames.Spillz.Scripts.UI
{
    public class ChangeCameraForCanvas : MonoBehaviour
    {
        private void Start()
        {
            Canvas canvas = GetComponent<Canvas>();

            if (canvas != null)
            {
                CameraManager camera = ServiceLocator.Resolve<CameraManager>();

                if (camera != null)
                    canvas.worldCamera = camera.ResolveCamera();
            }
        }
    }
}
