using MyBox;

namespace Assets.HeadStart.Core.SceneService
{
    public enum SCENE
    {
        MainMenu,
        Game
    }

    public interface ISceneService
    {
        SceneReference GetScene(SCENE scene);
    }
}
