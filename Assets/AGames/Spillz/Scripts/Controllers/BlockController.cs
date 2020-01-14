using AGames.Spillz.Scripts.Other;
using ProjectCore.Misc;

namespace AGames.Spillz.Scripts.Controllers
{
    public class BlockController : CachedBehaviour
    {
        private bool _highlightStatus = false;
        private ComplexCube _paranet;

        private void Awake()
        {
            _paranet = GetComponentInParent<ComplexCube>();
        }

        public virtual void BlockDestroy()
        {
            var parentGameObject = _paranet.gameObject;
            if (parentGameObject.IsNotNull()) parentGameObject.SetActive(false);
        }

        public virtual void BlockHighlight()
        {
            if (_paranet.IsNotNull()) _paranet.Highlight();
        }
    }
}
