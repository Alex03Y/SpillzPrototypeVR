using UnityEngine;

namespace AGames.Spillz.Scripts.Controllers
{
    public class HandHighlightController : MonoBehaviour
    {
        [SerializeField] private Renderer[] _handsVisual;
        private static readonly int OutlineWidth = Shader.PropertyToID("_OutlineWidth");
        private MaterialPropertyBlock _propertyBlock;
        private bool _highlighted = true;
    
        private void Start()
        {
            _propertyBlock = new MaterialPropertyBlock();
        }

        private void Update()
        {
            var buttonFour = OVRInput.GetDown(OVRInput.Button.Two);

            if (buttonFour)
            {
                Highlight();
            }
        }

        public void Highlight()
        {
            var outline = _highlighted ? 0.005f : 0f;
            foreach (var visual in _handsVisual)
            {
                visual.GetPropertyBlock(_propertyBlock);
                _propertyBlock.SetFloat(OutlineWidth, outline);
                visual.SetPropertyBlock(_propertyBlock);
            }
            _highlighted = !_highlighted;
        }
    }
}
