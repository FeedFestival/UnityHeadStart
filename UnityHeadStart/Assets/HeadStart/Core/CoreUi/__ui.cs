using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.HeadStart.CoreUi
{
    public static class __ui
    {
        private static Dictionary<UiDependency, IUiDependency> _uiCoreDependencies = new Dictionary<UiDependency, IUiDependency>();

        public static void Register(UiDependency dependency, CoreUiObservedValue obj)
        {
            if (_uiCoreDependencies.ContainsKey(dependency))
            {
                _uiCoreDependencies[dependency].Register(obj);
            }
        }

        public static void SetAvailable(UiDependency dependency, IUiDependency uiDependency)
        {
            _uiCoreDependencies.Add(dependency, uiDependency);
        }
    }
}