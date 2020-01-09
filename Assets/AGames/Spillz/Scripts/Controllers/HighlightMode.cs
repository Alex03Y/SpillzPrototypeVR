using ProjectCore.Misc;
using ProjectCore.ServiceLocator;
using UnityEngine;
using GameManager = AGames.Spillz.Scripts.Manager.GameManager;

namespace AGames.Spillz.Scripts.Controllers
{
    public class HighlightMode : CachedBehaviour
    {
        private GameManager _gameManager;
        private string _tagName;
        private void Start()
        {
            _gameManager = ServiceLocator.Resolve<GameManager>();
            _tagName = _gameManager.TagName;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_tagName))
            {
                
            }
        }
    }
}
