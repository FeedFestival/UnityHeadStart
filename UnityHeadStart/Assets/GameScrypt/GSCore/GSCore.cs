using GameScrypt.Core.Timeout;
using GameScrypt.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace GameScrypt.Core
{
    public class GSCore : MonoBehaviour, ICore
    {
        private Dictionary<string, GameObject> _gamescryptCoreSystems;
        private int _idCount = -1;
        private IUnit _player;

        private void Awake()
        {
            this.init();
            DontDestroyOnLoad(this);
        }

        private void init()
        {
            this.createTimeoutDependency();
            this.loadGameScryptCore();
        }

        public int GenerateId()
        {
            _idCount++;
            return _idCount;
        }

        public void SetPlayer(IUnit unit)
        {
            _player = unit;
        }

        public void InjectSystem(string system)
        {
            var go = GameObject.Instantiate(_gamescryptCoreSystems[system]);
            go.name = $"{ system } [DDOL]";
            go.GetComponent<IInventorySystem>().Init();
            _gamescryptCoreSystems.Remove(system);
        }

        private void createTimeoutDependency()
        {
            GameObject go = new GameObject("GSTimeout");
            go.transform.SetParent(this.transform);
            __.Timeout = go.AddComponent<GSTimeout>();
        }

        private void loadGameScryptCore()
        {
            var gameScryptCore = Resources.Load<GSExtraDependencies>("GameScrypt.Systems");
            _gamescryptCoreSystems = new Dictionary<string, GameObject>();
            foreach (var deps in gameScryptCore.List)
            {
                _gamescryptCoreSystems.Add(deps.Dependency, deps.Prefab);
            }
        }
    }
}