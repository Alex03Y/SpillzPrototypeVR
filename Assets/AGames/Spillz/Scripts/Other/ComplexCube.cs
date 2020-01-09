using ProjectCore.Misc;
using UnityEngine;

namespace AGames.Spillz.Scripts.Other
{
//    [RequireComponent(typeof(Rigidbody))]
    public class ComplexCube : CachedBehaviour
    {
        protected Renderer[] _childVisuals;
        private Renderer _renderer;
        private static readonly int OutlineWidth = Shader.PropertyToID("_OutlineWidth");
        private MaterialPropertyBlock _propertyBlock;
        private bool _highlighted = true;
        private Mesh _resultMesh;

        private void Awake()
        {
            ReMesh();
        }

        public virtual void ReMesh()
        {
            _propertyBlock = new MaterialPropertyBlock();
        }

        private void Start()
        {
            CacheVisuals();
        }


        public void Highlight()
        {
            var outline = _highlighted ? 0.008f : 0f;
            foreach (var visual in _childVisuals)
            {
                if (visual.IsNotNull())
                {
                    visual.GetPropertyBlock(_propertyBlock);
                    _propertyBlock.SetFloat(OutlineWidth, outline);
                    visual.SetPropertyBlock(_propertyBlock);
                }
            }
            _highlighted = !_highlighted;
        }

//        [Button()]
        public virtual void CacheVisuals()
        {
            _childVisuals = GetComponentsInChildren<Renderer>();
        }

    }
}