using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameTester
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SceneGameTesterSO", order = 1)]
    public class SceneGameTesterSO : ScriptableObject
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        [Serializable]
        public struct SceneGameTesterDictionary
        {
            public string SceneName;
            public GameObject SceneTesterPrefab;
        }
        [SerializeField]
        private SceneGameTesterDictionary[] ScenesDefinition;
        private Dictionary<string, GameObject> _scenes;
        public Dictionary<string, GameObject> Scenes
        {
            get
            {
                if (_scenes == null)
                {
                    _scenes = new Dictionary<string, GameObject>();
                    foreach (SceneGameTesterDictionary sd in ScenesDefinition)
                    {
                        _scenes.Add(sd.SceneName, sd.SceneTesterPrefab);
                    }
                }
                return _scenes;
            }
        }
    }
}
