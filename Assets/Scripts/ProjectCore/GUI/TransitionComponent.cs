using System;
using ProjectCore.Misc;
using UnityEngine;

namespace ProjectCore.GUI
{
    public abstract class ATransitionComponent : CachedBehaviour
    {
        /// <summary>
        ///     Animate the specified target transform and execute CallWhenFinished when the animation is done.
        /// </summary>
        /// <param name="target">Target transform.</param>
        /// <param name="callWhenFinished">Delegate to be called when animation is finished.</param>
        public abstract void Animate(Transform target, Action callWhenFinished);
    }
}