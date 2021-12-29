using MyBox;
using UnityEngine;

namespace Assets.HeadStart.Core.SceneService
{
    public class SceneServiceBase : MonoBehaviour, IDependency, ISceneService
    {
        public SceneManagerSO SceneManager;
        SceneReference ISceneService.GetScene(SCENE scene)
        {
            return SceneManager.Scenes[scene];
        }

        void IDependency.Init()
        {
            
        }
    }
}