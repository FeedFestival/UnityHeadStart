using System.Collections.Generic;
using UnityEngine;

namespace Assets.HeadStart.Core
{
    public class IoCDependencyResolver : MonoBehaviour
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.7";
#pragma warning restore 0414 //
        public CoreDependencies CoreDependencies;
        public Dictionary<Dependency, GameObject> _dependencies;

        void Awake()
        {
            DontDestroyOnLoad(this);
        }

        internal IDependency Resolve(Dependency dependency)
        {
            if (_dependencies == null)
            {
                CreateDependencies();
            }
            var dependencyGo = GameObject.Instantiate(_dependencies[dependency]);
            dependencyGo.name = dependencyGo.name.Replace("(Clone)", "");
            dependencyGo.transform.SetParent(transform);
            var iDependency = dependencyGo.GetComponent<IDependency>();
            return iDependency;
        }

        private void CreateDependencies()
        {
            _dependencies = new Dictionary<Dependency, GameObject>();
            foreach (CoreDependencies.DependencyDictionary dd in CoreDependencies.List)
            {
                _dependencies.Add(dd.Dependency, dd.Go);
            }
        }

        internal void CreateSession(SessionOpts sessionOpts)
        {
            var go = GameObject.Instantiate(CoreDependencies.CoreSession);
            go.name = go.name.Replace("(Clone)", "[" + sessionOpts.User.Name + "]");
            go.GetComponent<CoreSession>().SessionOpts = sessionOpts;
        }
    }
}
