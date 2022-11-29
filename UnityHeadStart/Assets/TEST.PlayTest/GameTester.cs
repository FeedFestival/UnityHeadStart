using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GameTester
{
    public class GameTester : MonoBehaviour
    {
        private static GameTester _instance;
        public static GameTester S { get { return _instance; } }

        public string LoadedScene;
        public bool HasTester;
        public SceneGameTesterSO SceneGameTesterSO;
        // public Dictionary<string, bool> TestedScenes = new Dictionary<string, bool>();
        public List<string> TestedScenes = new List<string>();
        public Dictionary<int, bool> Phases = new Dictionary<int, bool>();
        public Dictionary<int, Action> TestPhases = new Dictionary<int, Action>();

        void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LoadedScene = scene.name;
            HasTester = SceneGameTesterSO.Scenes.ContainsKey(LoadedScene);
            if (HasTester)
            {
                var go = Instantiate(SceneGameTesterSO.Scenes[LoadedScene]);
                GameTestBase gameTest = go.GetComponent<GameTestBase>();
                gameTest.Test();
            }
        }
    }
}
