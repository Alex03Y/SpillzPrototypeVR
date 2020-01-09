using System.Collections.Generic;
using ProjectCore.Misc;

namespace ProjectCore.Input
{
    public interface IInputRegisterer
    {
        InputProcessorBase[] Inputs { get; }
    }
    
    public abstract class BaseInputRegisterer : CachedBehaviour
    {
        public abstract void GetInputs(out List<IInputRegisterer> inputs);
    }
}
