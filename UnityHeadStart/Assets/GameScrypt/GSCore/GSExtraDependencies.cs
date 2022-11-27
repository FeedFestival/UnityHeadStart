using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScrypt.Core
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ExtraDependencies", order = 1)]
    public class GSExtraDependencies : ScriptableObject
    {
        [Serializable]
        public struct DependencyDictionary
        {
            public string Dependency;
            public GameObject Prefab;
        }
        public DependencyDictionary[] List;
    }
}
