﻿using System;
using System.Collections.Generic;
using ProjectCore.Misc;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace ProjectCore.PoolManager
{
    public class PoolManager : CachedBehaviour, IService
    {
        public Type ServiceType => typeof(PoolManager);
        private readonly Dictionary<int, PoolQueue> _poolMap = new Dictionary<int, PoolQueue>();

        /// <summary>
        /// Create pool using certain prefab.l
        /// </summary>
        /// <param name="prefab">Pool prefab.</param>
        /// <param name="startSize">Initial pool size.</param>
        /// <param name="flexibleSize">Increase size if pool was ended.</param>
        /// <exception cref="Exception">thrown exception if pool already exist.</exception>
        public void CreatePool(GameObject prefab, int startSize, bool flexibleSize = false)
        {
            var poolKey = prefab.GetInstanceID();

            if (_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} already exist.");

            var pool = new PoolQueue(prefab, startSize, flexibleSize, Transform.Value);
            _poolMap.Add(poolKey, pool);
        }

        /// <summary>
        /// Grabbing object from pool.
        /// </summary>
        /// <param name="prefab">Prefab was being used for creating pool.</param>
        /// <returns>Return container for pool object</returns>
        /// <exception cref="Exception">thrown exception if pool not exist.</exception>
        public PoolObject InstantiateFromPool(GameObject prefab)
        {
            var poolKey = prefab.GetInstanceID();

            if (!_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} not found.");

            return _poolMap[poolKey].GetPoolObject();
        }

        public void DisposePool(GameObject prefab, float timeToDestroyPool = 0)
        {
            var poolKey = prefab.GetInstanceID();

            if (!_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} not found.");

            var pool = _poolMap[poolKey];
            _poolMap.Remove(poolKey);

            pool.DisposePool(timeToDestroyPool);
        }
    }
}