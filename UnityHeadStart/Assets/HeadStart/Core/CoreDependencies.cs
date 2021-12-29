using System;
using UnityEngine;

namespace Assets.HeadStart.Core
{
    [CreateAssetMenu(fileName = "Data", menuName = "HeadStart/CoreDependencies", order = 1)]
    public class CoreDependencies : ScriptableObject
    {
        public GameObject CoreSession;
        [Serializable]
        public struct DependencyDictionary
        {
            public Dependency Dependency;
            public GameObject Go;
        }
        public DependencyDictionary[] List;
    }
}
