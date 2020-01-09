using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectCore.PoolManager
{
    public class PoolQueue
    {
        private readonly Queue<PoolObject> _poolObjects = new Queue<PoolObject>();

        private readonly GameObject _prefab;
        private readonly Transform _poolParent;
        private readonly bool _flexibleSize;

        public PoolQueue(GameObject prefab, int startSize, bool flexibleSize, Transform poolRoot)
        {
            _prefab = prefab;
            _flexibleSize = flexibleSize;

            _poolParent = new GameObject($"Pool_{prefab.name}").transform;
            _poolParent.parent = poolRoot;

            for (var i = 0; i < startSize; i++) _poolObjects.Enqueue(CreatePoolObject(_prefab));
        }

        public PoolObject GetPoolObject()
        {
            var poolObject = _poolObjects.Dequeue();
            _poolObjects.Enqueue(poolObject);

            if (poolObject.GameObject.activeSelf && !_flexibleSize)
            {
                poolObject.Destroy();
            }
            else if (poolObject.GameObject.activeSelf && _flexibleSize)
            {
                poolObject = CreatePoolObject(_prefab);
                _poolObjects.Enqueue(poolObject);
            }

            poolObject.GameObject.SetActive(true);
            foreach (var iPoolObject in poolObject.PoolObjectScripts)
                iPoolObject.OnReuseObject(poolObject);

            return poolObject;
        }

        public void DisposePool(float timeToDestroyPool)
        {
            if (Math.Abs(timeToDestroyPool) < 0.0001f)
            {
                while (_poolObjects.Count != 0)
                    Object.Destroy(_poolObjects.Dequeue().GameObject);
            }
            else
            {
                var poolSize = _poolObjects.Count;
                var timePerObject = timeToDestroyPool / poolSize;
                DisposeRecursively(timePerObject);
            }
        }

        private void DisposeRecursively(float timePerObject)
        {
            if (_poolObjects.Count == 0) return;
            Timer.Register(timePerObject, () =>
            {
                Object.Destroy(_poolObjects.Dequeue().GameObject);
                DisposeRecursively(timePerObject);
            });
        }

        private PoolObject CreatePoolObject(GameObject prefab)
        {
            var instance = Object.Instantiate(prefab, _poolParent);
            instance.SetActive(false);

            return new PoolObject(instance, _poolParent);
        }
    }
}