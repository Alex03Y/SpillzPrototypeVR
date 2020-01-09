using ProjectCore.Misc;
using UnityEngine;

namespace AGames.Spillz.Scripts.Controllers
{
    public class TowerRotateController : CachedBehaviour
    {
        private Rigidbody[] _rigidbodies;

        private void Awake()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }

        public void StartRotate()
        {
            foreach (var VARIABLE in _rigidbodies)
            {
                if (VARIABLE.IsNotNull()) VARIABLE.isKinematic = true;
            }
        }

        public void EndRotation()
        {
            foreach (var VARIABLE in _rigidbodies)
            {
                if (VARIABLE.IsNotNull()) VARIABLE.isKinematic = false;
            }
        }
    }
}
