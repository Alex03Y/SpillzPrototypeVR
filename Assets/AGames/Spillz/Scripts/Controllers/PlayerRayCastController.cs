using AGames.Spillz.Scripts.Input.InputProcessors;
using AGames.Spillz.Scripts.Manager;
using AGames.Spillz.Scripts.Other;
using ProjectCore.Input;
using ProjectCore.Misc;
using ProjectCore.ServiceLocator;

namespace AGames.Spillz.Scripts.Controllers
{
    public class PlayerRayCastController : CachedBehaviour, IInputReceiver<RayCastArgs>
    {
        private GameManager _gameManager;
        private InputManager _inputManager;
        private HandHighlightController _hand;
        private bool notChangeHighlight;


        // Start is called before the first frame update
        void Start()
        {
            _gameManager = ServiceLocator.Resolve<GameManager>();
            _inputManager = ServiceLocator.Resolve<InputManager>();
            _inputManager.Subscribe(new InputReceiver<RayCastArgs>(this));
            _hand = _gameManager.HandHighlight;
        }

   
        public void Execute(RayCastArgs args)
        {
//            if (args._highlightMode && !notChangeHighlight)
//            {
//                _hand.Highlight(true);
//                notChangeHighlight = !notChangeHighlight;
//            } 
//            else if (!args._highlightMode && notChangeHighlight)
//            {
//                _hand.Highlight(false);
//                notChangeHighlight = !notChangeHighlight;
//            }
          
            if (args._hit.collider != null)
            {
                if (args._highlightMode)
                {
                    args._hit.collider.GetComponent<BlockController>().BlockHighlight();
                }
                else args._hit.collider.GetComponent<BlockController>().BlockDestroy();
                
            }
        }
    }
}
