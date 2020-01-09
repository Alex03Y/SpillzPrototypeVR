using ProjectCore.Misc;
using ProjectCore.ServiceLocator;
using UnityEngine;
using GameManager = AGames.Spillz.Scripts.Manager.GameManager;

namespace AGames.Spillz.Scripts.Controllers
{
    public class BottomController : MonoBehaviour
    {
        [SerializeField] private float _timeBeforeDestroy;

        private string _tagName;
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = ServiceLocator.Resolve<GameManager>();
            _tagName = _gameManager.TagName;
        }

        private void OnTriggerEnter(Collider other)
        {
            var obj = other.GetComponentInParent<Rigidbody>().gameObject;
            if (obj.IsNull()) obj = other.gameObject;
            obj.GetComponent<Transform>().parent = transform;
            if (obj.CompareTag(_tagName))
            { 
                Timer.Register(_timeBeforeDestroy, () =>
                {
                    if (obj.IsNotNull()) Destroy(obj);
                });
            }

        }
    }
}
