using System;
using ProjectCore.Misc;
using ProjectCore.ServiceLocator;
using UnityEngine;

namespace AGames.Spillz.Scripts.Manager
{
    public class ManagerLevel : CachedBehaviour, IService
    {
        public Type ServiceType => typeof(ManagerLevel);

        [SerializeField] private GameObject[] _levels;
        [SerializeField] private Transform _respawnPoint;
        
        private GameObject _currentLevel;
        private int _iteration;
        private int _count;
        private Transform _transformCurrentLevel;

        private void Start()
        {
            _currentLevel = Instantiate(_levels[0], _respawnPoint);
            _iteration++;
            _count = _levels.Length-1;
        }

//        private void Update()
//        {
//            var buttonNextLvl = OVRInput.GetDown(OVRInput.Button.Four);
//            var buttonRestart = OVRInput.GetDown(OVRInput.Button.Three);
//        
//            if (buttonNextLvl) NextLevel();
//            if (buttonRestart) RestartLevel();
//        
//        }

        public void NextLevel()
        {
//        Destroy(_currentLevel);
//        _currentLevel = Instantiate(_levels[_iteration], _respawnPoint);
            _respawnPoint.transform.rotation = Quaternion.identity;
            if (_iteration > _count) _iteration = 0;
            Destroy(_currentLevel);
            _currentLevel = Instantiate(_levels[_iteration], _respawnPoint);
            _iteration++;
        }

        public void RestartLevel()
        {
            _respawnPoint.transform.rotation = Quaternion.identity;
            Destroy(_currentLevel);
            _currentLevel = Instantiate(_levels[_iteration-1], _respawnPoint);
        }

    }
}
