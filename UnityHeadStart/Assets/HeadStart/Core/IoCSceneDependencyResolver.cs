using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.HeadStart.Core
{
    public class IoCSceneDependencyResolver : MonoBehaviour
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        public CoreDependencies CoreSceneDependencies;
        public Dictionary<Dependency, GameObject> _sceneDependencies;

        internal IDependency Resolve(Dependency dependency)
        {
            if (_sceneDependencies == null)
            {
                CreateDependencies();
            }
            var dependencyGo = GameObject.Instantiate(_sceneDependencies[dependency]);
            dependencyGo.name = dependencyGo.name.Replace("(Clone)", "");
            dependencyGo.transform.SetParent(transform);
            var iDependency = dependencyGo.GetComponent<IDependency>();
            return iDependency;
        }

        private void CreateDependencies()
        {
            _sceneDependencies = new Dictionary<Dependency, GameObject>();
            foreach (CoreDependencies.DependencyDictionary dd in CoreSceneDependencies.List)
            {
                _sceneDependencies.Add(dd.Dependency, dd.Go);
            }
        }
    }
}
