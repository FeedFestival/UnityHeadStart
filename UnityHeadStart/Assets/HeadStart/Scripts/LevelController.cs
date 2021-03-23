using UnityEngine;
using Assets.Scripts.LevelService;

public class LevelController : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "1.0.3";
#pragma warning restore 0414 //
    public bool DebugThis;
    public LevelType LevelType;
    public string LevelName;
    [SerializeField]
    public GameplayState GameplayState;
    public GameObject LevelGo;
    public EffectsPool EffectsPool;
    public ILevel Level;

    public void Init()
    {
        Debug.Log("LevelType: " + LevelType);
        if (LevelType == LevelType.TheGame)
        {
            PreStartGame();
            StartGame();
        }
    }

    public void PreStartGame()
    {
        Debug.Log("Level - Pre Start Game");
        EffectsPool.GenerateParticleControllers();
    }

    public void StartGame()
    {
        Debug.Log("Level - Start Game");
        Level = LevelGo.GetComponent<ILevel>();
        if (Level == null) {
            Debug.Log("No Level");
            return;
        }
        Level.StartLevel();
    }

    public void Restart()
    {
        Game._.Restart();
    }
}

public enum GameplayState
{
    Starting, DuringPlay, Failed, Finished
}

public enum LevelType
{
    MainMenu, Loading, TheGame
}