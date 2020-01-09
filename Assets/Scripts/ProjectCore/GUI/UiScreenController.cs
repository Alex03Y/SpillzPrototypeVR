using ProjectCore.Misc;
using UnityEngine;

namespace ProjectCore.GUI
{
    public abstract class AUiScreenController<T> : CachedBehaviour, IUiScreenController where T : IScreenProperties
    {
        [Header("Screen Animations")] [SerializeField]
        private ATransitionComponent _animIn;

        [SerializeField] private ATransitionComponent _animOut;

        [Header("Screen properties")] [SerializeField]
        protected T Properties;

        public bool IsVisible { get; private set; }
        public string ScreenId { get; set; }

        public void Show(IScreenProperties properties = null)
        {
            Properties = (T) properties;
        }

        protected abstract void OnPropertiesSet();
    }
}