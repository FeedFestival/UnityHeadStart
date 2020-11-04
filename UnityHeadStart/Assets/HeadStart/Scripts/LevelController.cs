using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.LevelService;
using Assets.Scripts.utils;
using System.Linq;

public class LevelController : MonoBehaviour
{
    #pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "1.0.1";
    #pragma warning restore 0414 //
    public bool DebugThis;
    public bool IsMainMenu;
    public string LevelName;
    [SerializeField]
    public GameplayState GameplayState;
    public GameObject LevelGo;
    public ILevel Level;

    public void Init()
    {
        UIController._.InitMainMenu(IsMainMenu);

        if (IsMainMenu == false)
        {
            PreStartGame();
            StartGame();
        }
    }

    public void PreStartGame()
    {
        Debug.Log("Level - Pre Start Game");
    }

    public void StartGame()
    {
        Debug.Log("Level - Start Game");
        Level = LevelGo.GetComponent<ILevel>();
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
