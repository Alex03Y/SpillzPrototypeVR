using ProjectCore.Misc;
using UnityEngine;

namespace AGames.Spillz.Scripts
{
    public class RbCenterOfMass : CachedBehaviour
    {
        [SerializeField] private Vector3 _centerOfMAss;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(Rigidbody.Value.centerOfMass);
            Rigidbody.Value.centerOfMass = _centerOfMAss;
            Debug.Log(Rigidbody.Value.centerOfMass);
        }
    }
}
