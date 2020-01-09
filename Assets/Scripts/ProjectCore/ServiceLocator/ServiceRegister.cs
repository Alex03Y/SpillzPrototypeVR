using System.Collections.Generic;
using System.Linq;
using ProjectCore.Misc;
using UnityEngine;

namespace ProjectCore.ServiceLocator
{
    public class ServiceRegister : CachedBehaviour
    {
        public bool IncludeInactive = true;
        public SearchType ServiceSearchType;
        public enum SearchType
        {
            InDepthHierarchy,
            WholeObject,
            FirstEntry
        }

        private void Awake()
        {
            List<IService> findServices = null;
            switch (ServiceSearchType)
            {
                case SearchType.WholeObject:
                    findServices = new List<IService>(GetComponents<IService>());
                    break;
                case SearchType.InDepthHierarchy:
                    findServices = new List<IService>(GetComponentsInChildren<IService>(IncludeInactive));
                    break;
                case SearchType.FirstEntry:
                    findServices = new List<IService> {GetComponent<IService>()};
                    break;
            }

            findServices?.Where(x => x != null).ToList().ForEach(ServiceLocator.Register);
        }
    }
    
    public abstract class BaseServiceRegister : CachedBehaviour
    {
        public List<ScriptableObject> ScriptableServices = new List<ScriptableObject>();

        public void GetServices(out List<IService> services)
        {
            services = new List<IService>(ScriptableServices.Select(x => (IService) x));
            Services(services);
        }

        protected abstract void Services(List<IService> services);

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (var i = 0; i < ScriptableServices.Count; i++)
            {
                if (!(ScriptableServices[i] is IService))
                {
                    ScriptableServices.RemoveAt(i);
                    i--;
                }
            }
        }
#endif
    }
}