using ProjectCore.Input;
using UnityEngine;

namespace AGames.Spillz.Scripts.Input.InputProcessors
{
    public class LevelControlArgs : IInputArgs
    {
        public readonly bool NextLevel;
        public readonly bool RestartLevel;
        public LevelControlArgs(bool nextLevel, bool restartLevel)
        {
            NextLevel = nextLevel;
            RestartLevel = restartLevel;
        }
    }


    public class InputProcessorLevelControlEditor : InputBaseSpillzEditor<LevelControlArgs>
    {
        protected override LevelControlArgs Update()
        {
            var nextLevel = UnityEngine.Input.GetKeyDown(KeyCode.N);
            var restartLevel = UnityEngine.Input.GetKeyDown(KeyCode.R);
            if (nextLevel || restartLevel) return new LevelControlArgs(nextLevel, restartLevel); 
            return null;
        }
    }
    
    public class InputProcessorLevelControlOculus : InputBaseSpillzOculus<LevelControlArgs>
    {
        protected override LevelControlArgs Update()
        {
            var nextLevel = OVRInput.GetDown(OVRInput.Button.Four);
            var restartLevel = OVRInput.GetDown(OVRInput.Button.Three);
            if (nextLevel || restartLevel) return new LevelControlArgs(nextLevel, restartLevel);
            return null;
        }
    }
}