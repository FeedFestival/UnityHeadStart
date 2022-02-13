using System.Collections.Generic;

namespace Assets.HeadStart.CoreUi
{
    public static class __ui
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        private static Dictionary<UiDependency, IUiDependency> _uiCoreDependencies = new Dictionary<UiDependency, IUiDependency>();
        private static Dictionary<int, IUiDependency> _uiCustomDependencies = new Dictionary<int, IUiDependency>();

        public static void Register(UiDependency dependency, CoreUiObservedValue obj)
        {
            if (_uiCoreDependencies.ContainsKey(dependency))
            {
                _uiCoreDependencies[dependency].ListenForChanges(obj);
            }
        }
        public static void Register(int customDependency, CoreUiObservedValue obj)
        {
            if (_uiCustomDependencies.ContainsKey(customDependency))
            {
                _uiCustomDependencies[customDependency].ListenForChanges(obj);
            }
        }

        public static void Init(UiDependency dependency, object obj)
        {
            if (_uiCoreDependencies.ContainsKey(dependency))
            {
                _uiCoreDependencies[dependency].InitDependency(obj);
            }
        }

        public static void Init(int customDependency, object obj)
        {
            if (_uiCustomDependencies.ContainsKey(customDependency))
            {
                _uiCustomDependencies[customDependency].InitDependency(obj);
            }
        }

        public static void SetAvailable(UiDependency dependency, IUiDependency uiDependency)
        {
            _uiCoreDependencies.Add(dependency, uiDependency);
        }
        public static void SetAvailable(int customDependency, IUiDependency uiDependency)
        {
            if (_uiCustomDependencies.ContainsKey(customDependency))
            {
                _uiCustomDependencies[customDependency] = uiDependency;
                return;
            }
            _uiCustomDependencies.Add(customDependency, uiDependency);
        }

        public static void SetUnavailable(UiDependency dependency)
        {
            _uiCoreDependencies.Remove(dependency);
        }
        public static void SetUnavailable(int customDependency)
        {
            _uiCustomDependencies.Remove(customDependency);
        }
    }
}