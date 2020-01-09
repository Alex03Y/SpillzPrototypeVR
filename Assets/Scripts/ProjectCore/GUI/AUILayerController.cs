using System.Collections.Generic;
using ProjectCore.Misc;

namespace ProjectCore.GUI
{
    public abstract class AUiLayerController<T> : CachedBehaviour where T : IUiScreenController
    {
        protected Dictionary<string, T> ScreenControllers;

        public abstract void ShowScreen(T screen);
        public abstract void ShowScreen<TP>(T screen, TP properties) where TP : IScreenProperties;
        public abstract void HideScreen(T screen);
    }
}