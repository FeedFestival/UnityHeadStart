using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelController : MonoBehaviour
{
    public bool DebugThis;
    public bool IsMainMenu;
    public string LevelName;
    [SerializeField]
    public GameplayState GameplayState;

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
