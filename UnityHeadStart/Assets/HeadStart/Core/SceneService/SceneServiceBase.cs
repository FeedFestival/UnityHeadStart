using MyBox;
using UnityEngine;

namespace Assets.HeadStart.Core.SceneService
{
    public class SceneServiceBase : MonoBehaviour, ISceneService
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
        public SceneManagerSO SceneManager;
        SceneReference ISceneService.GetScene(SCENE scene)
        {
            return SceneManager.Scenes[scene];
        }
    }
}