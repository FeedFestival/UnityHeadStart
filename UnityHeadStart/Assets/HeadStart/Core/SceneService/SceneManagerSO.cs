using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace Assets.HeadStart.Core.SceneService
{
    [CreateAssetMenu(fileName = "SceneManagerSO", menuName = "ScriptableObjects/SceneManagerSO", order = 1)]
    public class SceneManagerSO : ScriptableObject
    {
        [Serializable]
        public struct SceneDictionary
        {
            public SCENE Scene;
            public SceneReference SceneRef;
        }
        [SerializeField]
        private SceneDictionary[] ScenesDefinition;
        private Dictionary<SCENE, SceneReference> _scenes;
        public Dictionary<SCENE, SceneReference> Scenes
        {
            get
            {
                if (_scenes == null)
                {
                    _scenes = new Dictionary<SCENE, SceneReference>();
                    foreach (SceneDictionary sd in ScenesDefinition)
                    {
                        _scenes.Add(sd.Scene, sd.SceneRef);
                    }
                }
                return _scenes;
            }
        }
    }
}