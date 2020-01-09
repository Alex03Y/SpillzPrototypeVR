using System;
using System.Collections.Generic;
using ProjectCore.Input;
using ProjectCore.ServiceLocator;

namespace AGames.Spillz.Scripts.Input
{
    public class InputRegistererSpillzScene : BaseInputRegisterer, IService
    {
        public Type ServiceType => typeof(InputRegistererSpillzScene);

        public override void GetInputs(out List<IInputRegisterer> inputs)
        {
            inputs = new List<IInputRegisterer>
            {
                new InputRegistereMoveDirection(),
                new InputRegistererRayCast(),
                new InputRegistererMouseScroll(),
                new InputRegistererRotation(),
                new InputRegistererLevelControll()
            };
        }

    }
}