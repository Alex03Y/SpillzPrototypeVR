using System.Collections.Generic;
using UnityEngine;

namespace AGames.Spillz.Scripts.Other
{
    public class ComplexCubeReMesh : ComplexCube
    {
        public override void ReMesh()
        {
            base.ReMesh();
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

        public override void CacheVisuals()
        {
//        _childVisuals[0] = GetComponent<Renderer>();
        }

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
            var _resultMesh = new Mesh();
            _resultMesh.CombineMeshes(list.ToArray(), false, false);

            // It is a good idea to clean unused meshes now
            foreach (var m in list)
            {
                Destroy(m.mesh);
            }

            return _resultMesh;
        }

    }
}
