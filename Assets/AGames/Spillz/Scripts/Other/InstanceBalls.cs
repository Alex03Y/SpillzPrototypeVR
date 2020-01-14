using System.Collections;
using ProjectCore.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AGames.Spillz.Scripts.Other
{
    public class InstanceBalls : CachedBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float timeSpawn;
        [SerializeField] private Transform TransformParent;
        [SerializeField] private float size;
        [SerializeField] private float radius;
        
        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);
            var parent = new GameObject("Balls").transform;
            parent.position = Transform.Value.position;
            parent.parent = Transform.Value;
            
            for (int i = 0; i < size; i++)
            {
                var instance = Instantiate(prefab, parent);
                instance.transform.position += Random.insideUnitSphere * radius;
                //instance.GetComponent<Rigidbody>().AddTorque(Random.Range(-5f, 5f), 0f, 0f);
                yield return new WaitForSeconds(timeSpawn);
            }
        
        }
    }
}
