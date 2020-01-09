using AGames.Spillz.Scripts.Input.InputProcessors;
using ProjectCore.Input;

namespace AGames.Spillz.Scripts.Input
{
    public class InputRegistereMoveDirection : IInputRegisterer
    {
        public InputProcessorBase[] Inputs { get; } = 
        {
            new InputProcessorMoveDirectionEditor()
//            new InputProcessorMoveDirectionOculus()
        };
    }

    public class InputRegistererRayCast : IInputRegisterer
    {
        public InputProcessorBase[] Inputs { get; } =
        {
            new InputProcessorRayCastEditor(),
            new InputProcessorRayCastOculus()
        };
    }
    
    public class InputRegistererRotation : IInputRegisterer
    {
        public InputProcessorBase[] Inputs { get; } =
        {
            //new InputProcessorRotationEditor()
            new InputProcessorRotationVelocity()
//            new InputProcessorRotationOculus()
        };
    }
    
    public class InputRegistererMouseScroll : IInputRegisterer
    {
        public InputProcessorBase[] Inputs { get; } =
        {
            new InputProcessorMouseScroll()
        };
    }
    
    public class InputRegistererLevelControll : IInputRegisterer
    {
        public InputProcessorBase[] Inputs { get; } =
        {
            new InputProcessorLevelControlEditor(),
            new InputProcessorLevelControlOculus()
        };
    }
}