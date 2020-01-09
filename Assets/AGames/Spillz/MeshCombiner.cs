using System.Collections.Generic;
using ProjectCore.Misc;
using UnityEngine;

public class MeshCombiner : CachedBehaviour
{
    public Rigidbody Rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        var meshes = GetComponentsInChildren<MeshFilter>();
        var material = GetComponentInChildren<MeshRenderer>().sharedMaterial;
        
        var filter = gameObject.AddComponent<MeshFilter>();
        var rend = gameObject.AddComponent<MeshRenderer>();

        filter.sharedMesh = CombineMeshes(meshes);
        rend.sharedMaterial = material;
        
        foreach (var mesh in meshes)
        {
            Destroy(mesh.GetComponent<MeshRenderer>());
            Destroy(mesh);
        }
    }

//    private void OnDrawGizmos()
//    {
//        if (Rigidbody != null)
//        {
//            var center = Rigidbody.centerOfMass;
//            center = transform.InverseTransformPoint(center);
//            Gizmos.DrawWireSphere(center, 0.2f);
//        }
//    }

    private Mesh CombineMeshes(MeshFilter[] meshes) 
    {
        var meshMap = new Dictionary<int, List<CombineInstance>>();

        foreach (var meshFilter in meshes) 
        {
            if (!meshMap.TryGetValue(meshFilter.sharedMesh.GetInstanceID(), out var instances)) 
            {
                instances = new List<CombineInstance>();
                meshMap.Add(meshFilter.sharedMesh.GetInstanceID(), instances);
            }

            var combineInstance = new CombineInstance();
            combineInstance.mesh = meshFilter.sharedMesh;
            combineInstance.transform = meshFilter.transform.localToWorldMatrix;
            instances.Add(combineInstance);
        }

        // Combine meshes and build combine instance for combined meshes
        var list = new List<CombineInstance>();
        
        foreach (var combined in meshMap) 
        {
            var m = new Mesh();
            m.CombineMeshes(combined.Value.ToArray());
            var ci = new CombineInstance();
            ci.mesh = m;
            list.Add(ci);
        }

        // And now combine everything
        var result = new Mesh();
        result.CombineMeshes(list.ToArray(), false, false);

        // It is a good idea to clean unused meshes now
        foreach (var m in list)
        {
            Destroy(m.mesh);
        }

        return result;

    }
}
