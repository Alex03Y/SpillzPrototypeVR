using UnityEngine;

namespace AGames.Spillz.Scripts.Other
{
    public class ChangeBlock : BlockController
    {
        private Material _material;
        private MaterialPropertyBlock _block;
        private bool _check = true;
        private static readonly int OutlineWidth = Shader.PropertyToID("_OutlineWidth");

        private void Awake()
        {
            _block = new MaterialPropertyBlock();
        }

        public override void BlockDestroy()
        {
            var render = GetComponent<Renderer>();
            render.GetPropertyBlock(_block);
            _block.SetFloat(OutlineWidth, _check ? 0.008f : 0); 
            render.SetPropertyBlock(_block);
            _check = !_check;
        }
    }
}
