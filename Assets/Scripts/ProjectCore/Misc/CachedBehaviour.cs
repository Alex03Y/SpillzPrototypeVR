using UnityEngine;

namespace ProjectCore.Misc
{
    public class CachedBehaviour : MonoBehaviour
    {
        public CachedComponent<Transform> Transform;
        public CachedComponent<Animator> Animator;
        public CachedComponent<Collider> Collider;
        public CachedComponent<Rigidbody> Rigidbody;

//        private GameObject _gameObject;
//
//        public GameObject GameObject
//        {
//            get
//            {
//                if (_gameObject.IsNull() && _gameObject == null) _gameObject = gameObject;
//                return _gameObject;
//            }
//        }

        private int _instanceId;
        public int InstanceId
        {
            get
            {
                if (_instanceId == 0) _instanceId = GetInstanceID();
                return _instanceId;
            }
        }

        public CachedBehaviour()
        {
            Transform = new CachedComponent<Transform>(this);
            Animator = new CachedComponent<Animator>(this);
            Collider = new CachedComponent<Collider>(this);
            Rigidbody = new CachedComponent<Rigidbody>(this);
        }
    }

    public class CachedComponent<T> where T : Component
    {
        private T _value;

        public T Value
        {
            get
            {
                if (_value == null) _value = _owner.GetComponent<T>();
                return _value;
            }
        }

        private int _instanceId;

        public int InstanceId
        {
            get
            {
                if (_instanceId == 0) _instanceId = _value.GetInstanceID();
                return _instanceId;
            }
        }

        private readonly Component _owner;

        public CachedComponent(Component owner)
        {
            _owner = owner;
        }
    }
}