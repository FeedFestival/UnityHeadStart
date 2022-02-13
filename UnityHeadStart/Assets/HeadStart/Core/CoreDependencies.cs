using System;
using UnityEngine;

namespace Assets.HeadStart.Core
{
    [CreateAssetMenu(fileName = "Data", menuName = "HeadStart/CoreDependencies", order = 1)]
    public class CoreDependencies : ScriptableObject
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
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
