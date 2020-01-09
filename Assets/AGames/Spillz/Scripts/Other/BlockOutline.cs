
namespace AGames.Spillz.Scripts.Other
{
    public class BlockOutline : BlockController
    {
        public override void BlockDestroy()
        {
            var complexCube = Transform.Value.GetComponentInParent<ComplexCube>();
//            if (complexCube.IsNotNull()) complexCube.Hide();
        }
    }
}