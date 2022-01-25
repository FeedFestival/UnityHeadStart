using System.Collections.Generic;
using Assets.HeadStart.Core.SceneService;
using Assets.HeadStart.Features.Dialog;
using UnityEngine;

namespace Assets.HeadStart.Core
{
    public static class CoreIoC
    {
        public static IoCDependencyResolver IoCDependencyResolver;
        private static Dictionary<Dependency, bool> _initializedDependencies = new Dictionary<Dependency, bool>();

        private static void CreateIoCResolver()
        {
            string coreExtension = string.Empty;
            bool hasCoreExtension = false;
            if (string.IsNullOrWhiteSpace(Main._.CoreExtension) == false)
            {
                hasCoreExtension = true;
                coreExtension = Main._.CoreExtension;
            }
            GameObject drGo = Object.Instantiate(Resources.Load(coreExtension + "DependencyResolver")) as GameObject;
            if (hasCoreExtension && drGo == null)
            {
                Debug.LogWarning("<b> NO (" + coreExtension + ") Extension found for </b>"
                    + " \"HeadStart/Resources/DependencyResolver\". Selecting default one. ");
                drGo = Object.Instantiate(Resources.Load("DependencyResolver")) as GameObject;
            }
            drGo.name = "____________dependencies____________";
            IoCDependencyResolver = drGo.GetComponent<IoCDependencyResolver>();
        }

        public static void Inject(Dependency dependency)
        {
            if (IoCDependencyResolver == null)
            {
                CreateIoCResolver();
            }
            if (_initializedDependencies.ContainsKey(dependency))
            {
                return;
            }

            IDependency iDependency = IoCDependencyResolver.Resolve(dependency);
            iDependency.Init();
            __.BootstrapDependency(dependency, iDependency);

            _initializedDependencies.Add(dependency, true);
        }
    }

    public static class CoreSceneIoC
    {
        public static IoCSceneDependencyResolver IoCDependencyResolver;
        private static Dictionary<Dependency, bool> _initializedDependencies = new Dictionary<Dependency, bool>();

        private static void CreateIoCResolver()
        {
            string coreExtension = string.Empty;
            bool hasCoreExtension = false;
            if (string.IsNullOrWhiteSpace(Main._.CoreExtension) == false)
            {
                hasCoreExtension = true;
                coreExtension = Main._.CoreExtension;
            }
            GameObject drGo = Object.Instantiate(Resources.Load(coreExtension + "SceneDependencyResolver")) as GameObject;
            if (hasCoreExtension && drGo == null)
            {
                Debug.LogWarning("<b> NO (" + coreExtension + ") Extension found for </b>"
                    + " \"HeadStart/Resources/SceneDependencyResolver\". Selecting default one. ");
                drGo = Object.Instantiate(Resources.Load("SceneDependencyResolver")) as GameObject;
            }
            drGo.name = "_________sceneDependencies__________";
            IoCDependencyResolver = drGo.GetComponent<IoCSceneDependencyResolver>();
        }

        public static void Inject(Dependency dependency)
        {
            if (IoCDependencyResolver == null)
            {
                CreateIoCResolver();
            }
            if (_initializedDependencies.ContainsKey(dependency))
            {
                return;
            }

            IDependency iDependency = IoCDependencyResolver.Resolve(dependency);
            iDependency.Init();
            __.BootstrapSceneDependency(dependency, iDependency);

            _initializedDependencies.Add(dependency, true);
        }

        internal static void ClearDependencies()
        {
            IoCDependencyResolver = null;
            _initializedDependencies.Clear();
        }
    }

    public enum Dependency
    {
        SFX, Time, Transition, SceneService, Dialog
    }

    public static class __
    {
        private static CoreEvent _event;
        public static CoreEvent Event
        {
            get
            {
                if (_event == null)
                {
                    _event = new CoreEvent();
                }
                return _event;
            }
        }
        public static ITime Time
        {
            get
            {
                FixDependency(Dependency.Time);
                return _coreDependencies[Dependency.Time] as ITime;
            }
        }
        public static ISFX SFX
        {
            get
            {
                FixDependency(Dependency.SFX);
                return _coreDependencies[Dependency.SFX] as ISFX;
            }
        }
        public static ITransition Transition
        {
            get
            {
                FixDependency(Dependency.Transition);
                return _coreDependencies[Dependency.Transition] as ITransition;
            }
        }
        public static ISceneService SceneService
        {
            get
            {
                FixDependency(Dependency.SceneService);
                return _coreDependencies[Dependency.SceneService] as ISceneService;
            }
        }
        public static IDialog Dialog
        {
            get
            {
                FixSceneDependency(Dependency.Dialog);
                return _sceneDependencies[Dependency.Dialog] as IDialog;
            }
        }

        private static Dictionary<Dependency, IDependency> _coreDependencies = new Dictionary<Dependency, IDependency>();
        private static Dictionary<Dependency, IDependency> _sceneDependencies = new Dictionary<Dependency, IDependency>();

        public static void BootstrapDependency(Dependency dependency, IDependency iDependency)
        {
            _coreDependencies.Add(dependency, iDependency);
        }

        public static void BootstrapSceneDependency(Dependency dependency, IDependency iDependency)
        {
            _sceneDependencies.Add(dependency, iDependency);
        }

        private static void FixDependency(Dependency dependency)
        {
            if (_coreDependencies.ContainsKey(dependency))
            {
                return;
            }

            CoreIoC.Inject(dependency);
        }

        private static void FixSceneDependency(Dependency dependency)
        {
            if (_sceneDependencies.ContainsKey(dependency))
            {
                return;
            }

            CoreSceneIoC.Inject(dependency);
        }

        internal static void ClearSceneDependencies()
        {
            _sceneDependencies.Clear();
            CoreSceneIoC.ClearDependencies();
        }
    }
}